# BuzzLock
<b> Team members: Rishov Sarkar, Joshua Shafran, Andrew Gauker, and Celeste Smith</b><br/>
The most secure door lock. <br/>

Living in the dorms and having to carry around a key can be annoying. Students lose their keys all the time and they take up space. So with the BuzzLock you can use Bluetooth and/or a BuzzCard** to unlock your door, no key required!

** Any magnetic stripe card can be used for authentication. **

<H3> Components Used </H3>
 <ul>
  <li> <a href="https://www.raspberrypi.org/products/raspberry-pi-3-model-b/">Raspberry Pi 3</a></li>
 <li> <a href="https://www.adafruit.com/product/2407">Touchscreen 7 inch HDMI Screen</a></li>
  <li> <a href="https://www.sparkfun.com/products/11884">HS-422 Servo</a></li>
  <li><a href="https://www.amazon.com/2xhome-Magnetic-Registry-Register-Quickbook/dp/B00E85TH9I/ref=sr_1_10?crid=UIZM18I37O7M&keywords=magstripe%2Breader&qid=1584997590&sprefix=%2Caps%2C227&sr=8-10&th=1">Magnetic Stripe Card Reader</a></li>
</ul> 

<H3> Schematic and Block Diagram </H3>

Schematic with servo connecting to Raspberry Pi
 <div style="text-align:center"><img src="Documentation/4180 Schematic Window.png" width="700" align="middle" alt="BuzzLock Schematic"></div>

 Block diagram of all component connections
 <img src="Documentation/Screen Shot 2020-04-28 at 3.59.49 PM.png" alt="BuzzLock Block Diagram"> 


<H3> Code Description </H3>

Our GUI is implemented with Windows Forms (.NET/C#) and designed in Microsoft Visual Studio. <a href = "https://www.mono-project.com/">Mono</a> is used to open the GUI on the Raspberry Pi. We used C# for the base functionality, along with SQL for the backend database to store user data during runtime and across power cycles. <a href = "https://unosquare.github.io/raspberryio/">Unosquare's</a> soft PWM function was used to control the servo for opening and closing the lock. The automatic on-screen keyboard for editing text fields such as name, phone number, PIN, etc. is provided by calling <a href="http://t-sato.in.coocan.jp/xvkbd/">xvkbd</a> using shell calls directly from C#. 

The USB magnetic stripe card reader acts as a keyboard. We use the unique formatting of magnetic card strings to identify a card swipe during any point in the program's execution. 

<H5> Authentication Methods </H5>

For ultimate security, we enforce a two factor authentication system in which users can unlock the door by combining two out of three different authentication methods: swiping a card, entering a pin and coming into range with a recognized bluetooth device. Users can change their authentication methods any time they access the system.

<H5> Permissions system </H5>

For finer granularity in who is able to use the system, we came up with a permission system. There are currently 3 possible permission levels a User may have:

- **NONE:** Cannot unlock the door or access the options menu, but their information is stored in the database.
- **LIMITED:** Can unlock the door and access the options menu, but cannot change other user's information.
- **FULL:** Can unlock the door, access the user management system and change any user's information.

The first user to initialize the system receives a FULL permission level. Subsequent users receive NONE permissions by default, though an administrator can later upgrade a user's permission level. 

<H5> State Diagram </H5>

The program is implemented as a state machine with 11 states, whose diagram you can see below. During runtime, each state transition updates the GUI accordingly. 

<img src="Documentation/4180 State Diagram.png" alt="State Diagram">

<H5> Windows Forms </H5>

In further detail, we approach this problem with an object oriented mindset. We created 4 Windows Forms: FormBuzzLock (a base class from which our 3 other forms inherit), FormStart, FormOptions, and FormUserManagement, which carry out the state machine's execution. 

- **FormStart** implements all the states in which the door is locked. 
- **FormOptions** allows users with LIMITED permissions to change their name, **phone number**, and authentication methods. 
- **FormUserManagement** allows users with FULL permissions to edit any user's profile, authentication methods and permissions, in addition to adding and removing users.




<H3> User Instructions </H3>
<H5> Initialization </H5>

Upon startup, this device will be uninitialized and display the status message "Hello! Please swipe your BuzzCard to begin set up." Once the user swipes a magstripe card, the initialization screen will display, allowing the owner of the system to input their information and preferences. Clicking the 'Save' button will save the user to the database. <br/>
The screen will now be in idle until an authentication method (card swipe or Bluetooth selected) is performed.  If the authentication method is already in the database, the user will be prompted for a second authentication method.  If the authentication method is not know, the user will be prompted to create a user account, but will not be able to open the door.  
Upon correct entry of the second authentication method and sufficient permissions (full or limited) the user will be able to open the lock and enter the the options menu to edit fields such as name, phone number, authentication methods, etc.  <br/>
If the authenticated user had full permissions they will be able to add and remove users from the options menu.  

