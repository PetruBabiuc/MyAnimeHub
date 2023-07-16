# MyAnimeHub
This is a client-server distributed application created with:
* C#
* Windows Forms
* Simple Sockets
* SQLite database
* [Newtonsoft JSON library](https://www.newtonsoft.com/json)

This project was made in collaboration with 3 of my college group colleagues. The application was made to improve our design skills focusing on SOLID principles and design patterns comprehension and usage. In our project we used the following desing patterns:
* State machine
* Factory
* Remote proxy
* Protection proxy

The next diagram presents application's use cases:
![Second phase request use case](https://github.com/PetruBabiuc/MyAnimeHub/blob/main/Diagrams/Use%20case%20diagrams/Second%20phase%20request%20use%20case.jpg)

The next class diagram that presents the architecture of the application.
![Class diagram](https://github.com/PetruBabiuc/MyAnimeHub/blob/main/Diagrams/Class%20diagrams/Class%20diagram.jpg)

The classes' colors have the following meaning:
| Class color | Present at server | Present at client |
| ----------- | ----------------- | ----------------- |
| Green       | ❌               | ✅                |
| Blue        | ✅               | ❌                |
| Cyan        | ✅               | ✅                |

The next activity diagram shows a high level overview of how the application works:
![Activity diagram](https://github.com/PetruBabiuc/MyAnimeHub/blob/main/Diagrams/Activity%20diagrams/Activity%20diagram.jpg)

The next images are taken from the application's [SRS document](https://docs.google.com/document/d/1gjAQD592itmmvchTFQm1cZ3Dx15qvpYSEQ0DteTIeQ0/edit) and present client's GUI:

![image](https://github.com/PetruBabiuc/MyAnimeHub/assets/100276291/286a4fbb-8bab-485d-a75f-17def768f3c6 "Welcome view")

![image](https://github.com/PetruBabiuc/MyAnimeHub/assets/100276291/3f3bf8e2-4839-4365-991c-2f916612f1c8 "Register view")

![image](https://github.com/PetruBabiuc/MyAnimeHub/assets/100276291/cb1c3e04-f1c0-4c85-9498-aab319879ee5 "Login view")

![image](https://github.com/PetruBabiuc/MyAnimeHub/assets/100276291/00c467f6-f9b0-46fb-8286-ba89e41bd7e1 "News view")

![image](https://github.com/PetruBabiuc/MyAnimeHub/assets/100276291/71d9dfcd-7bc4-49c9-959b-dbff4b5fae4f "Profile view")

![image](https://github.com/PetruBabiuc/MyAnimeHub/assets/100276291/2b643fe4-e3ef-4719-8bc2-6f17255cba04 "Search view")

![image](https://github.com/PetruBabiuc/MyAnimeHub/assets/100276291/ed1b2703-d79e-4431-8fe9-2602105f3e29 "Anime view")


## More information
The project has a [SRS document](https://docs.google.com/document/d/1gjAQD592itmmvchTFQm1cZ3Dx15qvpYSEQ0DteTIeQ0/edit). The contributions of each member can be found [here](https://docs.google.com/spreadsheets/d/1KACzTpyU_KAoAobhSnUuWRCFiHrdzBEkiKKd1053b9A/edit#gid=0).
