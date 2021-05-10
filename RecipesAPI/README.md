# React Recipes

Info:
1. [Story](#story)
2. [About](#about)
3. [Routes](#routes)
4. [Components](#Components)



## Story
The story behind. I wanted to use my backend knowledge right after I finished my studies in codecool Full-Stack course. In Codecool we usually work on a team project and I wanted to make a solo project for a better view of all the parts of the application.

## About
This is a backend part of project Recipes, about recipes, as part of [Pet Projects](https://github.com/mateszathmari/PetProjects)<br> 

* Communicating my React frontend [React frontend](https://github.com/mateszathmari/PetProjects/tree/master/recipes).
* Using Axios for fetching data.
* Entity framework for storing data.
* Storing user's salted and hashed password and the unique salt for user.


## Routes

### /login

Require "username" and "password" and in case of successful login return a Token for validating next communication.

### /logout

Require "username" and "Token" and in case of successful identification deleting a Token from database and loggin out.


### /registration

Require
* username (unique)
* password
* email 	(unique)
* city
* street
* house number
* post code

and in case of success saving data to database.

### /delete

Require "username" and "password" and in case of successful autentication deleting the user from database.

*** Development in progress ***
