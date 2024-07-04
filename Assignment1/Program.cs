static async Task Main(string[] args)
{
    Console.WriteLine("Async action program");
    await Task.Run(async () => // Changed to async lambda to await Task.Delay
    {
        await Task.Delay(1000);
        Console.Out.WriteLine("delayed message part 1");
    });

    await Task.Run(async () => // Added await to ensure this task is completed before the program exits
    {
        await Task.Delay(1000);
        Console.Out.WriteLine("delayed message part 2");
    });
    Console.WriteLine("Post task message");

    await Task.Run(() => { Console.Out.WriteLine("async action completed"); });
}

await Main(args);