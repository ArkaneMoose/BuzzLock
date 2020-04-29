# BuzzLock
<b> Team members: Rishov Sarkar, Joshua Shafran, Andrew Gauker, and Celeste Smith</b><br/>
The most secure door lock. <br/>

Living in the dorms and having to carry around a key can be annoying. Students lose their keys all the time and they take up space. So with the BuzzLock you can use Bluetooth and/or a BuzzCard** to unlock your door, no key required!

** Any magnetic stripe card can be used for authentication. **

## Components Used
 
  - [Raspberry Pi 3](https://www.raspberrypi.org/products/raspberry-pi-3-model-b/)
  - [Adafruit 7" 800x480 HDMI Backpack (+AR1100 Touch Controller)](https://www.adafruit.com/product/2407)
  - [HS-422 Servo](https://www.sparkfun.com/products/11884)
  - [Magnetic Stripe Card Reader](https://www.amazon.com/2xhome-Magnetic-Registry-Register-Quickbook/dp/B00E85TH9I/ref=sr_1_10?crid=UIZM18I37O7M&keywords=magstripe%2Breader&qid=1584997590&sprefix=%2Caps%2C227&sr=8-10&th=1)

## Schematic and Block Diagram

Schematic with servo connecting to Raspberry Pi
![BuzzLock Schematic](https://github.com/ArkaneMoose/BuzzLock/blob/master/Documentation/4180%20Schematic%20Window.png)

 Block diagram of all component connections
 ![BuzzLock Block Diagram](https://github.com/ArkaneMoose/BuzzLock/blob/master/Documentation/4180%20Block%20Diagram.png)


## Code Description

Our GUI is implemented with Windows Forms (.NET/C#) and designed in Microsoft Visual Studio. [Mono](https://www.mono-project.com/) is used to open the GUI on the Raspberry Pi. We used C# for the base functionality, along with SQL for the backend database to store user data during runtime and across power cycles. [Unosquare's](https://unosquare.github.io/raspberryio/) soft PWM function was used to control the servo for opening and closing the lock. The automatic on-screen keyboard for editing text fields such as name, phone number, PIN, etc. is provided by calling [xvkbd](http://t-sato.in.coocan.jp/xvkbd/) using shell calls directly from C#. 

The USB magnetic stripe card reader acts as a keyboard. We use the unique formatting of magnetic card strings to identify a card swipe during any point in the program's execution. 

### Authentication Methods

For ultimate security, we enforce a two factor authentication system in which users can unlock the door by combining two out of three different authentication methods: swiping a card, entering a pin, and coming into range with a recognized Bluetooth device. Users are permitted to change their authentication methods any time they access the system.

### Permissions system

For finer granularity in who is able to use the system, we have a permission system. There are currently 3 possible permission levels a user may have:

- **NONE:** Cannot unlock the door or access the options menu, but their information is stored in the database.
- **LIMITED:** Can unlock the door and access the options menu, but cannot change other user's information.
- **FULL:** Can unlock the door, access the user management system, and change any user's information.

The first user to initialize the system receives a FULL permission level. Subsequent users receive NONE permissions by default, though an administrator can later upgrade a user's permission level. 

### State Diagram

The program is implemented as a state machine with 11 states, whose diagram you can see below. During runtime, each state transition updates the GUI accordingly. 

![State Diagram](https://github.com/ArkaneMoose/BuzzLock/blob/master/Documentation/4180%20State%20Diagram.png)

### Windows Forms

In further detail, we approach this problem with an object oriented mindset. We created 4 Windows Forms: FormBuzzLock (a base class from which our 3 other forms inherit), FormStart, FormOptions, and FormUserManagement, which carry out the state machine's execution. 

- **FormStart** implements all the states in which the door is locked. 
- **FormOptions** allows users with LIMITED permissions to change their name, phone number, and authentication methods. 
- **FormUserManagement** allows users with FULL permissions to edit any user's profile, authentication methods and permissions, in addition to adding and removing users.

### Backend

We have detailed documentation of the classes and methods used in our backend available at [this website](https://buzzlock-docs.netlify.app/api/buzzlockgui.backend). 

### Servo

When the user reaches the authenticated state the servo will rotate clockwise using Unosquare's wiring pi software PWM function on GPIO pin 13.  Upon reenterance to the idle state or all the users are deleted the servo will rotate the opposite way, returning to a locked state.  The servo lock vs unlock functionality can be see in the red and green highlighting in the above state diagram.  

## User Instructions
The Buzzlock goes through a series of setup stages when it is first set up. The steps for use are outlined below.  

![uninitialized](https://github.com/ArkaneMoose/BuzzLock/blob/master/Documentation/Screenshots/Uninitialized.png)  
This is where a user will start when no users have been added.  There are instruction to swipe their card and are taken to the initializing state.

### Initialization

![initilization](https://github.com/ArkaneMoose/BuzzLock/blob/master/Documentation/Screenshots/Initializing%20-%20Card%20Entered.png)  
This screen allows the owner of the system to input their information and preferences. Clicking the 'Save' button will save the user to the database.

### Idle

![idle](https://github.com/ArkaneMoose/BuzzLock/blob/master/Documentation/Screenshots/Idle%20-%20Multiple%20Bluetooth%20Devices.png)  
The screen will now be in idle until a primary authentication method (card swipe or Bluetooth selected) is performed. If the primary authentication method is already in the database, the user will be prompted for a secondary authentication method. If the primary authentication method is not known, the user will be prompted to create a new account. 

### Second Factor

![secondary authentication](https://github.com/ArkaneMoose/BuzzLock/blob/master/Documentation/Screenshots/Second%20Factor%20-%20Pin.png)  
When a known primary authentication is performed something similar to the above screen will be shown asking for their second authentication method (**card or pin**). The user has their tries total to correctly enter their secondary authentication method.  

### Authenticated

![authenticated](https://github.com/ArkaneMoose/BuzzLock/blob/master/Documentation/Screenshots/Authenticated%20-%2010%20sec.png)  
Upon correct entry of the secondary authentication method and sufficient permissions (full or limited), the user will be able to open the lock. From here, the user has the ability to enter the options menu or lock the door immediately. If no action is taken, the door will automatically lock in 10 seconds.

### Options - Limited User

![options limited](https://github.com/ArkaneMoose/BuzzLock/blob/master/Documentation/Screenshots/Options%20-%20Limited%20User.png)  
A user with limited permissions can edit fields such as name, phone number, and authentication methods related to their profile as well as delete their profile.

### Options - User Management

![options user management](https://github.com/ArkaneMoose/BuzzLock/blob/master/Documentation/Screenshots/User%20Management%20-%20Full%2C%20Limited%2C%20None.png)  
A user with full permissions is able to add, remove and adjust any user profile within the database from the options menu.  They can also delete all users by deleting themselves. This will direct them back to the uninitialized screen, the system will be reset.  

### Access Denied

![access denied](https://github.com/ArkaneMoose/BuzzLock/blob/master/Documentation/Screenshots/Access%20Denied%20-%2010%20sec.png)  
If a user with no permissions logs in they will be shown the access denied screen. This screen will also apprear if the second factor is entered incorretly three times.  

## Future Work

This semester came with significant limitations, but we have many ideas for future expansions on the project:
  - Have users with permissions "FULL" be notified by text message or email when a user with permissions "NONE" attempts to enter the system
  - Implement encryption of user data and other security features
  - Connecting it to a real dorm style door
  - Implementing a logging system of every attempt to access the system (audit log).
  - Sensing when the door is actually closed to better synchronize the auto-lock feature
  - Sleep/Wake functionality
  - Enable the ability for the user to add a profile picture using the Raspberry Pi Camera
