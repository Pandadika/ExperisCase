using ExperiaCase.Models;

// Database mappen ligges i exe folderen, for at gøre det nemt.
// Load in data
string path = Directory.GetCurrentDirectory() + "/DB/";
string[] Users = File.ReadAllLines(path + "Users.txt");
string[] Products = File.ReadAllLines(path + "Products.txt");
int count = 0;

// Create user list with user objects
List<User> UsersList = new List<User>();
foreach (var item in Users)
{
    string[] values = item.Split(',');
    List<int> shown  = new List<int>();
    List<int> bought = new List<int>();

    foreach (string value in values[2].Split(';'))
    {
        shown.Add(int.Parse(value));
    }
    foreach (string value in values[3].Split(';'))
    {
        bought.Add(int.Parse(value));
    }

    UsersList.Add(
        new User(values[0], values[1], shown , bought)
        );
}

//Create film databse as product objects
Library FilmLibrary = new Library();
FilmLibrary.Films = new List<Product>();
foreach (var item in Products)
{
    string[] substring = item.Split(',');
    List<string> genres = new List<string>();
    //Adding keywords
    for (int i = 3; i <= 7; i++)
    {
        if (!string.IsNullOrWhiteSpace(substring[i]))
        {
            genres.Add(substring[i]);
        }
    }

    FilmLibrary.Films.Add(
        new Product(int.Parse(substring[0]), substring[1], int.Parse(substring[2]), genres, double.Parse(substring[8],System.Globalization.CultureInfo.InvariantCulture), double.Parse(substring[9]))
        );
}

// adding number of times showed and bought to products
int ids = Products.Length + 1;
foreach (User user in UsersList)
{
    foreach (int boughtItem in user._boughtList)
    {
        for (int id = 1; id < ids; id++)
        {
            if (boughtItem == id)
            {
                FilmLibrary.Films[id - 1].TimesBought++;
            }
        }
    }
    foreach (int shownItem in user._shownList)
    {
        for (int id = 1; id < ids; id++)
        {
            if (shownItem == id)
            {
                FilmLibrary.Films[id - 1].TimesViewed++;
            }
        }
    }
}

/// <summary>
/// Showing number bought
/// </summary>
/*int[] NumberBought = new int[Products.Length+1];
foreach (User user in UsersList)
{
    foreach (int boughtItem in user._boughtList)
    {
        for (int i = 1; i < NumberBought.Length; i++)
        {
            if (boughtItem == i)
            {
                NumberBought[i]++;
                FilmLibrary.Films[i - 1].TimesBought++;
            }
        }
    }
}*/

/// <summary>
/// Number of times shown
/// </summary>
/*
int[] NumberShown = new int[Products.Length + 1];
foreach (User user in UsersList)
{
    foreach (int shownItem in user._shownList)
    {
        for (int i = 1; i < NumberShown.Length; i++)
        {
            if (shownItem == i)
            {
                NumberShown[i]++;
                FilmLibrary.Films[i - 1].TimesViewed++;
            }
        }
    }
}*/


// Custom sorting
FilmLibrary.Films.Sort(delegate (Product x, Product y)
{
    return y.TimesBought.CompareTo(x.TimesBought);
});

Console.WriteLine("Search by Word in SearchWords ex Action");
FilmLibrary.Films.Sort();
foreach (Product item in FilmLibrary.Films)
{
    if (item._searchWord.Exists(s => s.Contains("Action")))
    {
        Console.WriteLine(item.GetName()+" has Action");
    }
}


FilmLibrary.SortByMostViewed();
Console.WriteLine("Most viewed");
for (int i = 0; i < 3; i++)
{
    Console.WriteLine(FilmLibrary.Films[i].GetName()+" Viewed "+ FilmLibrary.Films[i].TimesViewed);
}

FilmLibrary.SortByHigestReview();
Console.WriteLine("Highest reviews");
for (int i = 0; i < 3; i++)
{
    Console.WriteLine(FilmLibrary.Films[i].ToString());
    Console.WriteLine(FilmLibrary.Films[i].GetName()+" Score = "+ FilmLibrary.Films[i]._score);
}



FilmLibrary.SortByMostBought();
count= 0;
Console.WriteLine("Most bought Action");
foreach (var item in FilmLibrary.Films)
{
    if (item._searchWord.Exists(s => s.Contains("Action")) && count < 3)
    {
        count++;
        Console.WriteLine(item.GetName() + " has Action and was bought "+ item.TimesBought + " times");
    }
}


/*Console.WriteLine("Calculating mode");
for (int i = 1; i < NumberBought.Length; i++)
{
    Console.WriteLine($"id = {i}. Bought = {NumberBought[i]}");
}



Console.WriteLine("Calculating mode");
for (int i = 1; i < NumberShown.Length; i++)
{
    Console.WriteLine($"id = {i}. Shown = {NumberShown[i]}");
}
*/
/*
// fancy mode
List<int> vs1 = new List<int>();
foreach (User item in UsersList)
{
    foreach (int shownItem in item._shown)
    {
        vs1.Add(shownItem);
    }
}

var mode = vs1.GroupBy(n => n).OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault();
Console.WriteLine("Mode is " + mode);*/







// printing user list
foreach (var item in UsersList)
{
    Console.WriteLine(item.ToString());
}

/*
foreach (var item in FilmLibrary.Films)
{
    Console.WriteLine(item.ToString());
}


//Sorted by name.
FilmLibrary.Films.Sort();
foreach (var item in FilmLibrary.Films)
{
    Console.WriteLine(item.ToString());
}*/




//foreach (var item in Products)
//{
//    Console.WriteLine(item);
//}


//foreach (var item in Users)
//{
//    Console.WriteLine(item);
//}