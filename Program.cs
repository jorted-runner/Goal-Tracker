using System;

// I exceeded expectations by introducing some basic, error handling. If you make an incorrect choice, it lets you know.
// I also added a check to let the user know if they were about to overwrite a file and ask if they were sure.
class Program
{
    static void Main(string[] args)
    {
        UserInterface _userInterface = new UserInterface();
        bool _programRunning = true;
        while (_programRunning) {
            string menuChoice = _userInterface.DisplayMenu();
            _programRunning = _userInterface.InterfaceBrain(menuChoice);

        }
        
        
    }
}