# Spring
</br>Our new project is built to serve MVVM pattern and Oracle sql commands with new .dll easy static funcs.</br></br>
<p align="center">
<kbd>
<img src="springTM.ico" alt="Glass-Type-Copy-5-1366x697" 
style="corner-radius: 4; border: 5; border-color: blue;">
</kbd>
</p>
</br>

<!-- ABOUT THE PROJECT -->
## About The Project: 💼</br>
* It took 2 years to combine binding properties and commands [MVVM] with a/an speed and reliability UI experience</br>we use dotNet concepts ex. weavers and memory management system with some dlls and tweaks.

</br></br>

<!-- DATABASE WE USE -->
## Database Version: 💉</br>
* In this project we use <a href="https://en.wikipedia.org/wiki/Oracle_Database">Oracle</a> Database through Oracle with (ManagedDataAccess.dll) and a C# pure dll (AccioOracleKit.dll) both can work together to achieve repeatitve queries as string  same as SQL* and
manage connection object.

</br>
<a href=""><img src="./2x-en.png" ></a>

<!-- SCREENS -->
## Screens shots: 📷</br>

* Login </br></br>
<a href=""><img src="./1_2.png" ></a>
* User page </br></br>
<a href=""><img src="./Zxa.png" ></a>

* PDF extract demo </br></br>
 <a href="https://www.youtube.com/watch?v=SRUvmEIImJY"><img src="https://assets-global.website-files.com/64949e4863d96e26a1da8386/64b948a998510a9d2ded206e_633c71ec29fb38a9b6f4e285_1wLMdlEwmUttkWF49wzS5pTs2ZAXFIriMprSEUYsfcqasXaDwd8LFup6vhVSA85hK5OxLi0je9uTc-n2kLVejTJWzOmv_geKBWGXTV3mkor2Ghq62ZzzATKwUqJsOJDxzpTx9NYD6EQ94ynpe25tluOJ5wYr1Rf_VcVbhFQlGbNQNEFJrTSv3TONYw.png"></a>
         
* Charts demo </br></br>
<a href=""><img src="./Screenshot 2024-03-24 232337.png" ></a>
</br>
* Design on Site </br></br>
<a href=""><img src="./Screenshot 2024-02-17 002307.png" ></a>
</br>


What you need to know:.🫡 </br>
#1 this SW using database managed dll for oracle version 10g.<br>
#2 you should know how to build all tables to act inside scripts.<br>
#3 working in adding .cs file to handle main SQL commands and quiries.<br>
</br></br>
<div>
- [View Architecture] 🎲
<img src="./controls.png">
<details>
  <summary>Icons pack used in right options butttons with their indices..</summary>
  <img src="./icons_right_options_0.png" alt="image-description"/>
  <img src="./icons_right_options_1.png" alt="image-description"/>
  <img src="./icons_right_options_2.png" alt="image-description"/>
</details>
<div>
- [Layers] 🪢
<img src="./controls_illust-02.png">
</div>
Very Impertant!</br>
#1 first thing first you must build your database.</br>
#2 I will put down the structure script you may need to change it.</br>
<a href="https://dbdiagram.io/d/65cb61dbac844320ae0acc30">🫳Link to ERD</a></br>

#3 we use 'params.info' for initialize the server parameters and some other data about the current corporation.</br>
<a href="https://imgbb.com/"><img src="./ilis.png" alt="expl" border="0" style="width: 200; height: 100%;"></a>
</br>
params.info :
````
 
**this file is to initialize the current database connection to make app connect to its server**
**don't try to move lines down or up this will miss the whole file just change data values**
[
#server_ip::127.0.0.1,
#port::1521,
#company_name::Haam Corporation,
#version::1.1.0,
#console::False,
]
````

<!-- Server Side -->
## Server Side 😎🚀<br>
- [x] We're working on a small tool will allow any IT help desk <br> to make easy .dmp file every time and control any session on server too. 
- [x] Note: you need to install VC++ Redist 2012 ~ 2022.
<a href="https://imgbb.com/"><img src="./springserv_sc.png" alt="expl" border="0" style="width: 200; height: 100%;"></a>



<!-- To Do -->
## To Do List: ⛏️</br>
- [x] Add params file in the project.
- [x] Convert Scripts.cs to .dll library. (Now migrating to .dll as library to use!) from "using AccioInventory.DBConnection;" to "using AccioOracleKit;"
- [x] Integrating QR code.
- [ ] Chart cards usercontrol.
- [ ] Make a parallel procesudre process for exporting db DUMP.

---------------