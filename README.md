## Unity Rubics Cube

Gra została napisana na platformę Android za pomocą Unity. 
![5](https://github.com/user-attachments/assets/25754064-296d-47bb-bbef-670d778a432e)

## Opis Projektu

Gra oferuje użytkownikowi możliwość układania kostki Rubika i rywalizowania z własnymi rekordami pod względem minimalnej liczby ruchów potrzebnych do jej rozwiązania.
Dodatkowo, użytkownik ma możliwość wprowadzenia kolorów swojej fizycznej kostki Rubika ręcznie lub, w przypadku odpowiedniego oświetlenia, zeskanowania jej za pomocą aparatu w telefonie.
Po zeskanowaniu, użytkownik ma do wyboru dwa algorytmy którymi gra ułoży kostkę, a następnie wyświetli sekwencję ruchów potrzebnych do ułożenia kostki.

## Elementy Gry

- **Tryb układania kostki**: Kostka zostaje losowo pomieszana, a następnie użytkownik ma za zadanie ją ułożyć w jak najmniejszej liczbie ruchów.
- **Tryb rozwiązywania kostki**:Użytkownik wprowadza kolory swojej fizycznej kostki Rubika, ręcznie lub za pomocą aparatu w telefonie. Następnie wybiera jeden z dwóch algorytmów (opisanych poniżej), którymi kostka zostanie ułożona, a sekwencja ruchów potrzebnych do jej ułożenia zostanie wyświetlona.
  Użytkownik może przechodzić przez sekwencję ruchów krok po kroku za pomocą przycisku "Next", co pozwala na dokładne śledzenie procesu.
- **Algorytm Lbl**: Autorski algorytm oparty na metodzie LBL (Layer by Layer), dostosowany do sposobu, w jaki sam układam kostkę. Jest mało efektywny pod względem liczby ruchów (średnio 160 ruchów), ale łatwy do nauczenia.
- **Algorytm Kociemba**: Zaawansowany i bardzo wydajny algorytm opracowany przez Herberta Kociembę, który układa kostkę w około 30 ruchach.
  Algorytm ten jest zbyt skomplikowany, by można go było nauczyć się na pamięć, i służy głównie do szybkiego ułożenia kostki.
- **Dodatkowe elementy**: Suwak do przyśpieszania ruchów kostki, przycisk do resetowania kostki do stanu ułożonego oraz strona z instrukcją obsługi aplikacji.


## Technologie

- **Unity**: Silnik do tworzenia gier i interaktywnych aplikacji 3D, używany do projektowania i implementacji rozgrywki oraz elementów wizualnych.
- **C#**: Język programowania użyty do implementacji logiki gry.
- **WebCamTexture**: Klasa w Unity, która umożliwia korzystanie z kamery urządzenia do przechwytywania obrazu w czasie rzeczywistym. W połączeniu z metodą **GetPixels**, pozwala na odczytywanie kolorów kostki Rubika.

## Autor
Projekt został stworzony przez: 
- Witold Woźniczka

## Galeria
<img src="https://github.com/user-attachments/assets/47e3de92-75f9-4ad3-a127-7c8bdee76ea3" width="23%"></img> 
<img src="https://github.com/user-attachments/assets/b3def6b2-8dca-443b-abfd-1bf01ed2c646" width="23%"></img> 
<img src="https://github.com/user-attachments/assets/e0a9d1e4-cde2-492d-a5ca-ae1e4e816ef8" width="23%"></img> 
<img src="https://github.com/user-attachments/assets/53ee7f3c-bc4e-403c-baa0-1ac7608daf07" width="23%"></img> 
<img src="https://github.com/user-attachments/assets/ca6a02e5-22d4-479a-a0a5-102fe2b84747" width="23%"></img> 
<img src="https://github.com/user-attachments/assets/34796b30-2058-43fc-ba15-e6018a37373f" width="23%"></img> 




