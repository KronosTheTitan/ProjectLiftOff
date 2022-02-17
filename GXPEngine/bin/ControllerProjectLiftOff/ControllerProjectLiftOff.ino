#include <Keyboard.h>
//
//


const int BUTTON_0 =  7;
const int BUTTON_1 =  8;

const int joystickX = A2;
const int joystickY = A3;

void setup() {
  Serial.begin( 9600 );
  pinMode(BUTTON_0, INPUT_PULLUP);
  pinMode(BUTTON_1, INPUT_PULLUP);
  Keyboard.begin();
}

void readJoystick() {
  Serial.print(analogRead(joystickY), DEC);
  if (analogRead(joystickY) > 530) {
    Keyboard.press( 217);
  } else if (analogRead(joystickY) < 490) {
    Keyboard.press(218);
  }
  Serial.print(analogRead(joystickY), DEC);
  Serial.print(",");
  delay(15);
}

void readClicks() {
  if (digitalRead(BUTTON_1) == HIGH) {
    Keyboard.press('w');
  } else if (digitalRead(BUTTON_0) == HIGH) {
    Keyboard.press(32);
  } else {
    Keyboard.releaseAll();
  }
}
void loop() {
  readJoystick();
  readClicks();
}
