# Lab 4 — Concordance

C# console application that builds a concordance for a multiline text.

For each word, the program displays:
- the total number of occurrences;
- the sorted, unique line numbers where the word appears.

Implementation uses Dictionary<string, (int Count, SortedSet<int> Lines)>.
Words are normalized to lowercase and punctuation is ignored.

## Run

```bash
dotnet run --project Lab4.csproj
```

## Run tests

```bash
dotnet run --project Lab4.csproj -- --test
```
