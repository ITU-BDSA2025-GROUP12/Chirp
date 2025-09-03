using System;
using System.Text.RegularExpressions;

//Filen skal indlæses så vi kan håndtere dataen, bruges SR til.
StreamReader sr = new StreamReader("chirp_cli_db.csv");

//Lave exception til file not found

//Læs hver linje, hvis næste ikke er null

string text = sr.ReadToEnd();
Console.WriteLine(text);

//Regex brugernavn = new Regex("[a-z]{4,5})");
// (?<brugernavn>[a-z]{4,5}),"(?<kommentar>[A-Za-z ,!:).]*)",(?<epoch timestamp>\d*) 