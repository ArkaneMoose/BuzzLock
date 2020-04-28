# BuzzLock
The most secure door lock. <br/>
Team members: Rishov Sarkar, Joshua Shafran, Andrew Gauker, and Celeste Smith

Living in the dorms and having to carry around a key can be annoying. Students lose their keys all the time and they take up space. So with the BuzzLock you can use Bluetooth and/or a BuzzCard** to unlock your door, no key required!

** Any magnetic stripe card can be used for authentication. **

<H3> Components Used </H3>
 <ul>
  <li> <a href="https://www.raspberrypi.org/products/raspberry-pi-3-model-b/">Raspberry Pi 3</a></li>
 <li> <a href="https://www.adafruit.com/product/2407">Touchscreen 7 inch HTMI Screen</a></li>
  <li> <a href="https://www.sparkfun.com/products/11884">HS-422 Servo</a></li>
  <li><a href="https://www.amazon.com/2xhome-Magnetic-Registry-Register-Quickbook/dp/B00E85TH9I/ref=sr_1_10?crid=UIZM18I37O7M&keywords=magstripe%2Breader&qid=1584997590&sprefix=%2Caps%2C227&sr=8-10&th=1">Magnetic Stripe Card Reader</a></li>
</ul> 

<H3> Schematic and Block Diagram </H3>

Schematic with servo connecting to Raspberry Pi
 <img src="Documentation/4180 Schematic Window.png" alt="BuzzLock Schematic"> 
 
 Block diagram of all component connections
 <img src="Documentation/Screen Shot 2020-04-27 at 8.40.37 PM.png" alt="BuzzLock Block Diagram"> 


<H3> Code Description </H3>

C# was used for the base functionality along with SQL for the backend database to store user data. <a href = "https://unosquare.github.io/raspberryio/">Unosquare's</a> soft PWM function was used to control the servo for opening and closing the lock.  The automatic keyboard for editing text fields such as name, phone number, PIN, etc. is provided using <a href="http://t-sato.in.coocan.jp/xvkbd/">xvkbd</a> and the command line from C#.  

<H3> User Instructions </H3>
<H5> Initialization </H5>
Upon startup, this device will be uninitialized and display the status message "Hello! Please swipe your BuzzCard to begin set up." Once the user swipes a magstripe card, the initialization screen will display, allowing the owner of the system to input their information and preferences. Clicking the 'Save' button will save the user to the database. 
