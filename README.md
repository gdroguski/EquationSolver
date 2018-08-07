# Equation Solver

The aim of this project was to create user-friendly C# application but this time it's purpose was to display and find the zero of the function put by the user in terms of <a href="https://www.codecogs.com/eqnedit.php?latex=x" target="_blank"><img src="https://latex.codecogs.com/gif.latex?x" title="x" /></a> on given closed interval and a with fixed error <a href="https://www.codecogs.com/eqnedit.php?latex=\varepsilon" target="_blank"><img src="https://latex.codecogs.com/gif.latex?\varepsilon" title="\varepsilon" /></a> (epsilon). It's made using MVVM pattern within WPF application and comparing to the SoundsOrganizer project is simpler one.

The most interesting feature of this project is in my opinion the evaluator of user's input which parses any combination of elementary mathematical functions such as trigonometric, cyclometric, hyperbolic etc. with, by default <a href="https://www.codecogs.com/eqnedit.php?latex=x" target="_blank"><img src="https://latex.codecogs.com/gif.latex?x" title="x" /></a> as the variable. Idea behind finding the zero of the function is in my opinion trivial - it's just a linear search with bounded error within points list which is required to draw a function anyway with fixed step size <a href="https://www.codecogs.com/eqnedit.php?latex=\Delta&space;x" target="_blank"><img src="https://latex.codecogs.com/gif.latex?\Delta&space;x" title="\Delta x" /></a>.


#### Example usage of the application:

<p align="center">
  <img src="https://raw.githubusercontent.com/gdroguski/EquationSolver/master/example.png">
</p>

Because of the mentioned triviality of finding the zero of the function, sometimes, when the function changes very rapidly on given interval we can achieve result not exactly in the <a href="https://www.codecogs.com/eqnedit.php?latex=y=0" target="_blank"><img src="https://latex.codecogs.com/gif.latex?y=0" title="y=0" /></a> or even no result at all. It's because of the linear search of the <a href="https://www.codecogs.com/eqnedit.php?latex=x" target="_blank"><img src="https://latex.codecogs.com/gif.latex?x" title="x" /></a>'s list. But in most cases and for my requirements in the past it performed very well. In the future it could be done to move all strings into resources and maybe change the method to Newton's or other iterative one.
