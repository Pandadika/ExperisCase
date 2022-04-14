using ExperiaCase.Models;


// Database mappen ligges i exe folderen, for at gøre det nemt.
// Load in data
string path = Directory.GetCurrentDirectory() + "/DB/";
string[] Users = File.ReadAllLines(path + "Users.txt");
string[] Products = File.ReadAllLines(path + "Products.txt");


// Create user list with user objects
List<User> UsersList = new List<User>();
foreach (var item in Users)
{
    string[] values = item.Split(',');
    UsersList.Add(
        new User(values[0], values[1], values[2].Split(';').ToList(), values[3].Split(';').ToList())
        );
}

Library FilmLibrary = new Library();
FilmLibrary.Films = new List<Product>();
foreach (var item in Products)
{
    string[] vs = item.Split(',');
    List<string> genres = new List<string>();
    for (int i = 3; i <= 7; i++)
    {
        if (!string.IsNullOrEmpty(vs[i]))
        {
            genres.Add(vs[i]);
        }
    }

    FilmLibrary.Films.Add(
        new Product(int.Parse(vs[0]), vs[1], int.Parse(vs[2]), genres, double.Parse(vs[8]), double.Parse(vs[9]))
        );
}




// printing user list
foreach (var item in UsersList)
{
    Console.WriteLine(item.ToString());
}

foreach (var item in FilmLibrary.Films)
{
    Console.WriteLine(item.ToString());
}




//foreach (var item in Products)
//{
//    Console.WriteLine(item);
//}


//foreach (var item in Users)
//{
//    Console.WriteLine(item);
//}