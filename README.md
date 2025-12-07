# Sztt Voting (Szavazóplatform)
<img width="1919" height="1018" alt="image" src="https://github.com/user-attachments/assets/b91c0c28-ee2a-4164-8eae-6bf459d7e2b4" />

Ez az alkalmazás a BME Szoftvertechnológia és technikák tárgy II. házi feladatának megoldását tartalmazza.
Az alkalmazás **.NET MAUI** alapú, és támogatja a **Windows10.0.19041.0** és **Mac Catalyst 9.0** platformokat.

## Rövid ismertető
Az alkalmazás egy egyszerű közösségi jellegű szavazóplatform, ahol regisztrált felhasználók különböző témákban szavazásokat hozhatnak létre, mások pedig ezeken részt vehetnek.
A rendszer nem publikus, csak regisztrált felhasználók férhetnek hozzá.

### Csapat
Csapatnév: OOP Enjoyers
- Végh Mátyás      - FEGRL2
- Fabriczius Dávid - LNC97H
- Horváth Simon    - DQUXW4
- Török Simon      - EN6EDS

## Telepítési útmutató
Győződjön meg róla, hogy telepítve van a **.NET 9 SDK**.
1. <ins>Repository letöltése:</ins>
   - Klónozza le a repository-t: git clone https://github.com/Matyassin/SzttVoting vagy használja az IDE vagy más külső szoftver által felkínált lehetőségeket.
     
2. <ins>A program futtatása:</ins>
   - Startup project-ként az SzttVoting projekt fájl lesz kijelölve, ha nem így történt jelölje ki ezt a projektet, mint "Startup project".
   - Futtassa a programot.

## Rövid használati útmutató
(A teljes dokumentációk a Documents mappában találhatóak.) <br><br>
Bejelentkezés után a felhasználó-t egy Welcome Page fogadja, ahol létrehozhat új szavazatokat, megnézheti az összes szavazást, megtekintheti saját profilja adatait, valamint ki is léphet a rendszerből.

- Új szavazat létrehozása:
  <img width="1875" height="941" alt="image" src="https://github.com/user-attachments/assets/055cdca8-5d1f-4eea-8882-5de42f58ff5e" />
  - Adott szavazat létrehozása után a felhasználó saját szavazata megtekinthető a View Polls gombra kattintva a Welcome Page-en.
 
- Szavazatok megtekintése:
  <img width="1831" height="893" alt="image" src="https://github.com/user-attachments/assets/21e7537e-cc35-4147-87f6-7223ee047723" />
  - Ezen az oldalon a felhasználó szavazhat még le nem zárt szavazásokra, módosíthatja saját szavazatait (ha azokra még nem érkezett szavazat), lezárhatja saját szavzásait és megtekintheti az Archívumban lévő szavazások statisztikáit is.
