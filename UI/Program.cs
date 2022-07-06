using Core.Models.ToDo;
using UI;

using var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7193/")
};

while (true)
{
    Console.WriteLine("Your toDoes:");

    var toDoes = (await Requests.Get(httpClient));

    for (int i = 0; i < toDoes.Count(); i++)
    {
        Console.WriteLine($"{i + 1} - {toDoes.ElementAt(i)}");
    }
    Console.WriteLine();

    Console.Write("You can: Add new(1), Update existing(2), Delete(3)\n Chose command:");
    var input = Console.ReadLine();

    if (input == "1")
    {
        while (true)
        {
            Console.Write("Data of your toDo = ");
            var data = Console.ReadLine();

            if (String.IsNullOrEmpty(data))
            {
                Console.WriteLine("Incorrect data! Try again\n");
                continue;
            }

            var toDO = new ToDo { Data = data };

            await Requests.Post(httpClient, toDO);
            break;
        }
    }

    else if (input == "2")
    {
        while (true)
        {
            Console.Write("ToDo`s id that you want to edit= ");
            var idStr = Console.ReadLine();

            if (!int.TryParse(idStr, out int id))
            {
                Console.WriteLine("Incorrect id! Try again\n");
            }

            Console.WriteLine($"Current toDo - {Requests.Get(httpClient, id).Result}");

            Console.Write("Data of your toDo = ");
            var data = Console.ReadLine();

            Console.Write("State of your toDo = ");
            var stateStr = Console.ReadLine();

            if (String.IsNullOrEmpty(data) || !int.TryParse(stateStr, out int state))
            {
                Console.WriteLine("Incorrect data! Try again\n");
                continue;
            }



            var toDO = new ToDo { Id = id, Data = data, State = (State)state };

            await Requests.Put(httpClient, toDO);
            break;
        }
    }

    else if (input == "3")
    {
        while (true)
        {
            Console.Write("ToDo`s id that you want to edit= ");
            var idStr = Console.ReadLine();

            if (!int.TryParse(idStr, out int id))
            {
                Console.WriteLine("Incorrect input! Try again\n");
                continue;
            }

            await Requests.Delete(httpClient, id);
            break;
        }
    }

    else
    {
        Console.WriteLine("Wrong input! Try again\n");
        continue;
    }
}