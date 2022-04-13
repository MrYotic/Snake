using static System.Console;
OutputEncoding = System.Text.Encoding.UTF8;
ConsoleKey direction = ConsoleKey.S;
Random rnd = new Random();//"DooD on top!".Select(z => (int)z).Sum()
(int x, int y) size = (24, 24);
List<(int x, int y)> snake = new List<(int x, int y)>() { (10, 10), (10, 11), (10, 12) };
(int x, int y) apple = GenApple();
new Thread(() =>
{
    while (true)
    {
        ConsoleKey key = ReadKey(true).Key;
        if ((key == ConsoleKey.W && direction != ConsoleKey.S) || (key == ConsoleKey.S && direction != ConsoleKey.W) || (key == ConsoleKey.A && direction != ConsoleKey.D) || (key == ConsoleKey.D && direction != ConsoleKey.A))
        {
            direction = key;
            Thread.Sleep(90);
        }
    }
}).Start();
new Thread(() =>
{
    while (true)
    {
        (int x, int y) newHead = ((snake.Last().x + (direction == ConsoleKey.A ? -1 : (direction == ConsoleKey.D ? 1 : 0))) % size.x, (snake.Last().y + (direction == ConsoleKey.W ? -1 : (direction == ConsoleKey.S ? 1 : 0))) % size.y);
        newHead = (newHead.x < 0 ? (size.x - Math.Abs(newHead.x)) : newHead.x, newHead.y < 0 ? (size.y - Math.Abs(newHead.y)) : newHead.y);
        SetPixel(newHead.x, newHead.y, ConsoleColor.Yellow);
        SetPixel(snake.Last().x, snake.Last().y, ConsoleColor.White);
        SetPixel(snake.First().x, snake.First().y, ConsoleColor.Black);
        if (snake.Contains(newHead)) Environment.Exit("Game over".Select(z => (int)z).Sum());
        else if (newHead == apple) apple = GenApple();
        else snake.RemoveAt(0);
        snake.Add(newHead);
        Thread.Sleep(100);
    }
}).Start();
void SetPixel(int x, int y, ConsoleColor color)
{
    ForegroundColor = color;
    SetCursorPosition((x + 2) * 2, y + 2);
    Write("██");
    SetCursorPosition(0, 0);
    ForegroundColor = ConsoleColor.White;
}
(int, int) GenApple()
{
    List<(int x, int y)> places = string.Join(',', Enumerable.Range(0, size.x).Select(x=>string.Join(',', Enumerable.Range(0, size.y).Select(y => x + " " + y)))).Split(',').Select(z=>(int.Parse(z.Split(' ')[0]), int.Parse(z.Split(' ')[1]))).Where(z=>!snake.Contains(z)).ToList();
    (int x, int y) place = places[rnd.Next(0, places.Count - 1)];
    SetPixel(place.x, place.y, ConsoleColor.Red);
    return place;
}