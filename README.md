# Equation Solver

The aim of this project was to create user-friendly C# application but this time it's purpose was to display and find the zero of the function put by the user in terms of *x* on given closed interval and a with fixed error. It's made using MVVM pattern within WPF application and comparing to the SoundOrganizer project is simpler one.
The most interesting feature of this project is in my opinion the evaluator of user's input which parses any combination of elementary mathematical functions such as trigonometric, cyclometric, hyperbolic etc. with, by default *x* as the variable. Idea behind finding the zero of the function is in my opinion trivial - it's just a linear search with bounded error within points list which is required to draw a function anyway with fixed spacing *dx*.


Example usage of the application:


![Alt text](https://bytebucket.org/groguski/equationsolver/raw/16663a9a46b3659f455777ab3eb96f9e0b5b682c/example.png )


Because of the mentioned triviality of finding the zero of the function, sometimes, when the function changes very rapidly on given interval we can achieve result not exactly in the *y=0* or even no result at all. It's because of the linear search of the *x*'s list. But in most cases and for my requirements in the past it performed very well. In the future it could be done to move all strings into resources and maybe change the method to Newton's or other iterative one.