Console.Clear();
List<string> StartingTasks = new List<string>();
List<string> CompleteTasks = new List<string>();
List<string> TasksToday = new List<string>();

string[] ToDoTasks = File.ReadAllLines("ToDoTasks.csv");
string[] CompTasks = File.ReadAllLines("CompletedTasks.csv");
string[] TodayTasks = File.ReadAllLines("Stats.csv");
Random r = new Random();
int random = r.Next(0, 9);
string[] Greetings = File.ReadAllLines("Greetings.csv");

int TasksCreated2Day = 0;
string greeting = Greetings[random];
Console.WriteLine(greeting);
DateTime StartTime = new DateTime();
DateTime CompletionTime = new DateTime();
DateTime Today = new DateTime();
Today = DateTime.Today;
Console.WriteLine(Today);

foreach (string items in ToDoTasks)
{
    StartingTasks.Add(items);
}
foreach (string items in CompTasks)
{
    CompleteTasks.Add(items);
}
foreach (string items in TodayTasks)
{
    TasksToday.Add(items);
}

void ToDoItems()
{
    Console.WriteLine("Here are your incomplete items");
    for (int x = 0; x < StartingTasks.Count; x++)
    {
        Console.WriteLine($"   {x + 1}: {StartingTasks[x]}");
    }
    if (StartingTasks.Count == 0)
        Console.WriteLine("You currently have no items on your list");
    Console.ReadLine();
    TaskMenu();
}

void CompletedTasks()
{
    Console.WriteLine("Here are your Completed Tasks");
    for (int x = 0; x < CompleteTasks.Count; x++)
    {
        Console.WriteLine($" {x + 1}: {CompleteTasks[x]}");
    }
    if (CompleteTasks.Count == 0)
        Console.WriteLine("You currently have no items on your list");
    Console.ReadLine();
    TaskMenu();
}

void RemoveTasks()
{
    if (StartingTasks.Count <= 0)
    {
        Console.WriteLine("You have no tasks, you have to create a task to remove it.");
        TaskMenu();
    }
    else
    {
        Console.WriteLine("Which Task would you like to remove?");

        for (int x = 0; x < StartingTasks.Count; x++)
        {
            Console.WriteLine($" {x + 1}: {StartingTasks[x]}");
        }
        Console.WriteLine("(Type the Number)");
        int NumComp = int.Parse(Console.ReadLine()) - 1;
        if (NumComp > StartingTasks.Count)
        {
            Console.WriteLine("Invalid Input");
            Console.ReadLine();
            TaskMenu();
        }
        Console.WriteLine($"Is this the Task that you want to Remove {StartingTasks[NumComp]}\n<y or n>");
        string? response = Console.ReadLine();
        if (response == "y")
        {
            Console.WriteLine("Okay, Item removed");
            StartingTasks.Remove(StartingTasks[NumComp]);
            TaskMenu();
        }
        if (response == "n")
        {
            Console.WriteLine("Okay, sounds good");
            TaskMenu();
        }
        Console.ReadLine();
        TaskMenu();
    }
}

void CompletingTasks()
{
    if(StartingTasks.Count <= 0){
        Console.WriteLine("You have no tasks to complete, please add a task to complete it.");
    }
    else{
    Console.WriteLine("Which Task would you like to complete?");
    for (int x = 0; x < StartingTasks.Count; x++)
    {
        Console.WriteLine($" {x + 1}: {StartingTasks[x]}");
    }
    Console.WriteLine("(Type the Number)");
    int NumComp = int.Parse(Console.ReadLine()) - 1;
    if (NumComp > StartingTasks.Count)
    {
        Console.WriteLine("Invalid Input");
        TaskMenu();
    }
    Console.WriteLine($"Is this the Task that you want to Complete {StartingTasks[NumComp]}\n<y or n>");
    string? response = Console.ReadLine();
    if (response == "y")
    {
        Console.WriteLine("Okay, Item Completed");
        CompletionTime = DateTime.Now;
        string TaskCompleted = $"{StartingTasks[NumComp]} End Time: {CompletionTime}";
        StartingTasks.Remove(StartingTasks[NumComp]);
        CompleteTasks.Add(TaskCompleted);
        TaskMenu();
    }
    if (response == "n")
    {
        Console.WriteLine("Okay, sounds good");
        TaskMenu();
    }
    }

    TaskMenu();
}

void Stats()
{
    int CurrentToDo = StartingTasks.Count;
    int CurrentComplete = CompleteTasks.Count;
    int TotalTasks = CurrentToDo + CurrentComplete;
    Console.WriteLine("Here are your stats.");
    Console.WriteLine($"Current Tasks: {CurrentToDo} \nTasks Completed: {CurrentComplete} \nTotal Tasks: {TotalTasks}");
    Console.WriteLine("Tasks Per day");
    for(int x = 0; x < TasksToday.Count; x++){
        Console.WriteLine($"{TasksToday[x]}");
    }
    Console.ReadLine();
    TaskMenu();
}

void AddingItems()
{
    Console.WriteLine("Which item would you like to add?");
    string? response = Console.ReadLine();
    string? Task = response;
    Console.WriteLine($"Here is the name of the item: {Task}");
    Console.WriteLine("What is the description?");
    response = Console.ReadLine();
    string Description = response;
    Console.WriteLine($"Here is the task: {Task}: {Description}");
    Console.WriteLine("Is this what you want?\n<y or n>");
    response = Console.ReadLine();
    if (response == "y")
    {
        Console.WriteLine("Okay, Item Added");
        StartTime = DateTime.Now;
        string TaskToBeDone = ($"{Task}:  {Description}    Start Time: {StartTime}     ");
        StartingTasks.Add(TaskToBeDone);
        TasksCreated2Day++;
        TaskMenu();
    }
    if (response == "n")
    {
        Console.WriteLine("Okay, Sounds Good");
        TaskMenu();
    }
}

void TaskMenu()
{
    Console.WriteLine("What would you like to do?");
    Console.WriteLine("1) See tasks to be done\n2) See Completed Tasks\n3) Add Items\n4) Remove Tasks\n5) Complete Tasks\n6) See Stats\n7) Be Done");
    string? response = Console.ReadLine();
    if (response == "1")
        ToDoItems();
    if (response == "2")
        CompletedTasks();
    if (response == "3")
        AddingItems();
    if (response == "4")
        RemoveTasks();
    if (response == "5")
        CompletingTasks();
    if (response == "6")
        Stats();
    if (response == "7")
    {
        File.WriteAllLines("ToDoTasks.csv", StartingTasks);
        File.WriteAllLines("CompletedTasks.csv", CompleteTasks);
        TasksToday.Add($"{TasksCreated2Day} Tasks Created on {Today}");
        File.WriteAllLines("Stats.csv", TasksToday);
        System.Environment.Exit(0);
    }
    else
    {
        TaskMenu();
    }
}

TaskMenu();
