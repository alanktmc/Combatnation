//Low-pass filter from Arduino library: Sketch -> Include Library -> Manage Libraries... -> Search for "MegunoLink" and download the latest file
double raw = 0.0;
double filter [50];
double sum = 0.0;
double getSensorAvg();
double sensitivity = 400.0;

#include "Filter.h"
#include "MegunoLink.h"

ExponentialFilter <double> Filter0(50,0);
ExponentialFilter <double> Filter1(50,0);
ExponentialFilter <double> Filter2(50,0);
ExponentialFilter <double> Filter3(50,0);
ExponentialFilter <double> Filter4(50,0);

void setup() {
  Serial.begin(9600);
  for (int i = 3; i < 8; i++)
  {
    pinMode(i, OUTPUT);
    digitalWrite(i, LOW);
  }
}

void loop() {
    Filter0.Filter(getSensorAvg(0));
    Filter1.Filter(getSensorAvg(1));
    Filter2.Filter(getSensorAvg(2));
    Filter3.Filter(getSensorAvg(3));
    Filter4.Filter(getSensorAvg(4));  // applying the filter to the avg value of every 50 samples (see the sampling filter at the end)
    // double filters are applied to the sensor value

    switch (Serial.read()){
      
      case'1':
            digitalWrite(3, HIGH);          
      break;
      
      case'2':
            digitalWrite(4, HIGH); 
      break;
      
      case'3':
            digitalWrite(5, HIGH);   
      break;
      
      case'4':
            digitalWrite(6, HIGH);   
      break;
      
      case'5':
            digitalWrite(7, HIGH);   
      break;
/*******************************************************/
      case'6':
            digitalWrite(3, LOW);

      break;
      
      case'7':
            digitalWrite(4, LOW);
      break;
      
      case'8':
            digitalWrite(5, LOW);
      break;
      
      case'9':
            digitalWrite(6, LOW);
      break;
      
      case'0':
            digitalWrite(7, LOW);
      break;
    }

    if(Filter0.Current()> sensitivity){
      Serial.println("A");
      digitalWrite(3, LOW);
      delay(500);
    }
    if(Filter1.Current()> sensitivity){
      Serial.println("B");
      digitalWrite(4, LOW);
      delay(500);
    }
    if(Filter2.Current()> sensitivity){
      Serial.println("C");
      digitalWrite(5, LOW);
      delay(500);
    }
    if(Filter3.Current()> sensitivity){
      Serial.println("D");
      digitalWrite(6, LOW);
      delay(500);
    }     
    if(Filter4.Current()> sensitivity){
      Serial.println("E");
      digitalWrite(7, LOW);
      delay(500);
    }
//    Serial.print(Filter0.Current());
//    Serial.print(" ");
//    Serial.print(Filter1.Current());
//    Serial.print(" ");
//    Serial.print(Filter2.Current());
//    Serial.print(" ");
//    Serial.print(Filter3.Current());
//    Serial.print(" ");
//    Serial.print(Filter4.Current());
//    Serial.println(" ");
}

double getSensorAvg(int analogPin)  // applying a sampling filter (take the average value of every 50 samples)
{
  for (int a = 0; a < 50; a++)
  {
    raw = analogRead(analogPin);
    filter [a] = raw;
    sum += filter[a];
  }
  double avg = sum/50.0;
  sum = 0;
  return avg;
}
