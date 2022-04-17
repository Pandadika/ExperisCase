using ExperiaCase.Models;
using ExperiaCase.Codes;

// Database mappen ligges i exe folderen, for at gøre det nemt.
// Load in data
string path = Directory.GetCurrentDirectory() + "/DB/";
string[] Users = File.ReadAllLines(path + "Users.txt");
string[] Products = File.ReadAllLines(path + "Products.txt");
string[] CurrentUserSession = File.ReadAllLines(path + "CurrentUserSession.txt");
int count = 0;
Random random = new Random();

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
        new User(int.Parse(values[0]), values[1], shown , bought)
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

// Making list of UserSessions
List<UserSession> userSessions = new List<UserSession>();
foreach (var item in CurrentUserSession)
{
    string[] values = item.Split(',');
    userSessions.Add(new UserSession() {userId = int.Parse(values[0]), viewing = int.Parse(values[1])}); 
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
/*FilmLibrary.SortByMostViewed();
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

FilmLibrary.TopFilms(3, "comedy", SearchCriteria.score);*/


/*List<Product> ActionProducts = new List<Product>();
ActionProducts = FilmLibrary.GetAllInGenre("Comedy");
Console.WriteLine("Comedy Movies");
foreach (var item in ActionProducts)
{
    Console.WriteLine(item.GetName());
}*/

Console.WriteLine("");
Console.WriteLine("Finding users hopefully");
foreach (var item in userSessions)
{
    Console.WriteLine(UsersList.Find(u => u._id.Equals(item.userId))._name+" Viewing "+ FilmLibrary.Films.Find(p => p._id.Equals(item.viewing)).GetName());
    Console.WriteLine(OtherUsersAlso(item.userId, item.viewing));
}

string OtherUsersAlso(int userid, int viewing)
{
    // finding the user, the products they are viewing and a list of other users who has bought it.
    string result = "";
    User currentUser = UsersList.Find(u => u._id.Equals(userid));
    Product currentViewing = FilmLibrary.Films.Find(p => p._id.Equals(viewing));
    List<User> otherUsers = UsersList.FindAll(u => u._boughtList.Exists(i => i == viewing));
    otherUsers.Remove(currentUser);
    List<Product> similar = new List<Product>();
    List<Product> otherUserProducts = new List<Product>();
    bool viewed = false;
    


    //if other user bought, make list of products bought.
    if (otherUsers.Any())
    {
        foreach (User user in otherUsers)
        {
            foreach (int p in user._boughtList)
            {
                if (!otherUserProducts.Contains(FilmLibrary.Films.Find(prod => prod._id.Equals(p))))
                {
                    otherUserProducts.Add(FilmLibrary.Films.Find(prod => prod._id.Equals(p)));
                }
            }
        }
        otherUserProducts.Remove(currentViewing);
        //Printing other bought items
        if (otherUserProducts.Any())
        {
            Console.WriteLine($"Other viewers who bought{currentViewing.GetName()} also bought");
            foreach (var item in otherUserProducts)
            {
                Console.WriteLine(item.GetName());
            }
        }
    }
    else // else making list of other users who viewed
    {
        viewed = true;  
        otherUsers = UsersList.FindAll(u => u._shownList.Exists(i => i == viewing));
        otherUsers.Remove(currentUser);
    }

    //if users viewed make list of products viewed.
    if (otherUsers.Any() && viewed)
    {
        foreach (User user in otherUsers)
        {
            foreach (int p in user._shownList)
            {
                if (!otherUserProducts.Contains(FilmLibrary.Films.Find(prod => prod._id.Equals(p))))
                {
                    otherUserProducts.Add(FilmLibrary.Films.Find(prod => prod._id.Equals(p)));
                }
            }
        }
        otherUserProducts.Remove(currentViewing);
        //printing other viewed items
        if (otherUserProducts.Any())
        {
            Console.WriteLine($"Other viewers who viewed{currentViewing.GetName()} also viewed");
            foreach (var item in otherUserProducts)
            {
                Console.WriteLine(item.GetName());
            }
        }
    }
    else
    {
        FilmLibrary.TopFilms(3, currentViewing._searchWord[random.Next(currentViewing._searchWord.Count)], SearchCriteria.bought);
        return "No similar users found";
    }


    




/*
    Console.WriteLine("current user");
    Console.WriteLine(currentUser._name);
    Console.WriteLine("Similar users");
    foreach (var item in otherUsers)
    {
        Console.WriteLine(item._name);
    }
    Console.WriteLine("Looking at");
    Console.WriteLine(currentViewing.GetName());
*/

    return result;
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
/*foreach (var item in UsersList)
{
    Console.WriteLine(item.ToString());
}*/

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