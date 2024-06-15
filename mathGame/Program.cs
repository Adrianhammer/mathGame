using System.Diagnostics;
using mathGame;

mathGameLogic mathGame = new mathGameLogic();
Random random = new Random();

int firstNum;
int secondNum;
int userSelection;
int score = 0;
bool gameOver = false;

difficultyLevel difficultyLevel = difficultyLevel.easy;

while (!gameOver)
{
    userSelection = getUserMenuSelection(mathGame);

    firstNum = random.Next(1, 101);
    secondNum = random.Next(1, 101);

    switch (userSelection)
    {
        case 1:
            score += await PerformOperation(mathGame, firstNum, secondNum, score, difficultyLevel, '+');
            break;
        case 2:
            score += await PerformOperation(mathGame, firstNum, secondNum, score, difficultyLevel, '-');
            break;
        case 3:
            score += await PerformOperation(mathGame, firstNum, secondNum, score, difficultyLevel, '*');
            break;
        case 4:
            while (firstNum % secondNum != 0)
            {
                firstNum = random.Next(1, 101);
                secondNum = random.Next(1, 101);
            }
            score += await PerformOperation(mathGame, firstNum, secondNum, score, difficultyLevel, '/');
            break;
        case 5:
            int numberOfQuestions = 99;
            System.Console.WriteLine("Please enter the number of questions you want to attempt.");
            while (!int.TryParse(Console.ReadLine(), out numberOfQuestions))
            {
                System.Console.WriteLine("Please enter a number of questions you want to attempt as an integer number.");
            }
            while (numberOfQuestions > 0)
            {
                int randomOperation = random.Next(1, 5);

                if (randomOperation == 1)
                {
                    firstNum = random.Next(1, 101);
                    secondNum = random.Next(1, 101);
                    score += await PerformOperation(mathGame, firstNum, secondNum, score, difficultyLevel, '+');
                }
                else if (randomOperation == 2)
                {
                    firstNum = random.Next(1, 101);
                    secondNum = random.Next(1, 101);
                    score += await PerformOperation(mathGame, firstNum, secondNum, score, difficultyLevel, '-');
                }
                else if (randomOperation == 3)
                {
                    firstNum = random.Next(1, 101);
                    secondNum = random.Next(1, 101);
                    score += await PerformOperation(mathGame, firstNum, secondNum, score, difficultyLevel, '*');
                }
                else
                {
                    firstNum = random.Next(1, 101);
                    secondNum = random.Next(1, 101);
                    while (firstNum % secondNum != 0)
                    {
                        firstNum = random.Next(1, 101);
                        secondNum = random.Next(1, 101);
                    }
                    score += await PerformOperation(mathGame, firstNum, secondNum, score, difficultyLevel, '/');
                }
                numberOfQuestions--;
            }
            break;
        case 6:
            Console.WriteLine("GAME HISTORY: \n");
            foreach (var operation in mathGame.gameHistory)
            {
                System.Console.WriteLine($"{operation}");
            }
            break;
        case 7:
            difficultyLevel = changeDiffLevel();
            difficultyLevel difficultyEnum = (difficultyLevel)difficultyLevel;
            Enum.IsDefined(typeof(difficultyLevel), difficultyEnum);
            System.Console.WriteLine($"Your new difficulty level is: {difficultyLevel}");
            break;

        case 8:
            gameOver = true;
            System.Console.WriteLine($"Your final score is {score}");
            break;
    }

}


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

static int getUserMenuSelection(mathGameLogic mathGame)
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

    return selection;
}

static async Task<int?> getUserResponse(difficultyLevel difficulty)
{
    int response = 0;
    int timeout = (int)difficulty;

    Stopwatch stopwatch = new Stopwatch();
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

        else
        {
            throw new OperationCanceledException();
        }
    }
    catch (OperationCanceledException)
    {
        System.Console.WriteLine("Time is up");
        return null;
    }
}

static int ValidateResult(int result, int? userResponse, int score)
{
    if (result == userResponse)
    {
        System.Console.WriteLine("You answer correctly! You earned 5 points.");
        score += 5;
    }
    else
    {
        System.Console.WriteLine("Try again!");
        System.Console.WriteLine($"Correct answer is {result}");
    }
    return score;
}

static async Task<int> PerformOperation(mathGameLogic mathGame, int firstNum, int secondNum, int score, difficultyLevel difficulty, char operation)
{
    int result;
    int? userResponse;
    displayMathQuestion(firstNum, secondNum, operation);
    result = mathGame.mathOperation(firstNum, secondNum, operation);
    userResponse = await getUserResponse(difficulty);
    score += ValidateResult(result, userResponse, score);
    return score;
}
