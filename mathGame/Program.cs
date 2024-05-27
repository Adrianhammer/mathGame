using System.Diagnostics;
using mathGame;

mathGameLogic mathGame = new mathGameLogic();
Random random = new Random();  

int firstNum;
int secondNum;
int userSelection;
int score = 0;
bool gameOver = false;


static difficultyLevel changeDiffLevel() 
{
    int userSelection = 1;

    System.Console.WriteLine("Your current Difficulty is Easy");
    System.Console.WriteLine("1. Easy");
    System.Console.WriteLine("2. Medium");
    System.Console.WriteLine("3. Hard");

    while (!int.TryParse(Console.ReadLine(), out userSelection) || (userSelection < 1 || userSelection > 3))
    {
        System.Console.WriteLine("Please enter a valid option: 1, 2, 3");
    }

    switch (userSelection)
    {
        case 1:
            return difficultyLevel.easy;
        case 2:
            return difficultyLevel.medium;
        case 3:
            return difficultyLevel.hard;
        default:
            return difficultyLevel.easy; // Default case to handle unexpected values
    }
}

static void displayMathQuestion(int firstNum, int secondNum, char operation) 
{
    System.Console.WriteLine($"{firstNum} {operation} {secondNum} = ??");
}

static void getUserMenuSelection(mathGameLogic mathGame) 
{
    int selection = -1;
    mathGame.showMenu();

    while (selection < 1 || selection > 8)
    {
        while (!int.TryParse(Console.ReadLine(), out selection))
        {
            System.Console.WriteLine("Please enter a valid option between 1-8");
        }
    }
}

static async Task<int?> getUserResponse(difficultyLevel difficulty)
{
    int response = 0;
    int timeout  = (int)difficulty;

    Stopwatch stopwatch= new Stopwatch();
    stopwatch.Start();

    Task<string?> getUserInputTask = Task.Run(() => Console.ReadLine());

    try
    {
        string? result = await Task.WhenAny(getUserInputTask, Task.Delay(timeout * 1000)) == getUserInputTask ? getUserInputTask.Result : null;

        stopwatch.Stop();

        if (result != null && int.TryParse(result, out response))
        {
            System.Console.WriteLine($"Time taken to answer: {stopwatch.Elapsed.ToString(@"m\::ss\.fff")}");
            return response;
        }
    }
    catch (System.Exception)
    {
        
        throw;
    }
}

