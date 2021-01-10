use biblioteka;

-- Mjesto
insert into Mjesto (Naziv, PostanskiBroj) VALUES ('Banja Luka', '78000');
insert into Mjesto (Naziv, PostanskiBroj) VALUES ('Doboj','78200');
insert into Mjesto (Naziv, PostanskiBroj) VALUES ('Trebinje', '78900');
insert into Mjesto (Naziv, PostanskiBroj) VALUES ('Bijeljina','78500');
insert into Mjesto (Naziv, PostanskiBroj) VALUES ('Istočno Sarajevo','78100');

-- Zanr
insert into Zanr (Naziv, Opis) VALUES ('horor','Samo za one sa jakim srcem...');
insert into Zanr (Naziv, Opis) VALUES ('psihologija','Tajne ljudske duše i uma...');
insert into Zanr (Naziv, Opis) VALUES ('poezija','Najljepši stiovi poezije...');
insert into Zanr (Naziv, Opis) VALUES ('istorija','Priče o minulim vremenima...');
insert into Zanr (Naziv, Opis) VALUES ('klasična književnost','Samo za odabrane ljubitelje knjiga...');

-- Autor
insert into Autor (IdMjesto, Ime, Prezime, DatumRodjenja) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Banja Luka'),'Petar', 'Kočić',"1877-04-09");
insert into Autor (IdMjesto, Ime, Prezime, DatumRodjenja) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Doboj'),'Marko', 'Maran',"1975-07-05");
insert into Autor (IdMjesto, Ime, Prezime, DatumRodjenja) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Trebinje'),'Zlatko', 'Vukota',"1965-05-03");
insert into Autor (IdMjesto, Ime, Prezime, DatumRodjenja) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Bijeljina'),'Ana', 'Ris',"1990-01-02");
insert into Autor (IdMjesto, Ime, Prezime, DatumRodjenja) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Istočno Sarajevo'),'Marija', 'Blažić',"1980-04-06");

-- Izdavac
insert into Izdavac (IdMjesto, Naziv, Adresa) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Banja Luka'),'Jović Print','Marije Bursać 45');
insert into Izdavac (IdMjesto, Naziv, Adresa) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Doboj'),'Grafotisk','Jovana Simića 25');
insert into Izdavac (IdMjesto, Naziv, Adresa) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Trebinje'),'Dučićtisk','Marije Savić 5');
insert into Izdavac (IdMjesto, Naziv, Adresa) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Bijeljina'),'Bijeljina Print Max','Jove Savić 32');
insert into Izdavac (IdMjesto, Naziv, Adresa) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Istočno Sarajevo'),'Jovanić Print','Ane Marić 68');

-- Knjiga
insert into Knjiga (IdZanr, IdIzdavac, Naslov, DatumObjavljivanja, ISBN, UkupanBrojKopija, BrojStranica, Jezik, Opis) VALUES
    (3, 1,'Jazavac pred sudom', '2000-05-07','546512321548456548754565452154', 100, 80, 'srpski', 'Jazavac pred sudom je...');
insert into Knjiga (IdZanr, IdIzdavac, Naslov, DatumObjavljivanja, ISBN, UkupanBrojKopija, BrojStranica, Jezik, Opis) VALUES
    (1,2,'Smrt u Veneciji', '2010-08-07','546512321548456548754561252154', 80, 300, 'srpski', 'Smrt u Veneciji je...');


-- Knjiga_ima_Autor
insert into Knjiga_ima_Autor (IdKnjiga, IdAutor) VALUES (1, 2);
insert into Knjiga_ima_Autor (IdKnjiga, IdAutor) VALUES (2, 3);

-- Osoba
insert into Osoba (IdMjesto, Ime, Prezime, Adresa, BrojTelefona) VALUES
    (1,'Jana','Maran', 'Trg Krajine 5', '065658965');
insert into Osoba (IdMjesto, Ime, Prezime, Adresa, BrojTelefona) VALUES
    (1,'Jovan','Marković', 'Trg Krajine 25', '065657775');
insert into Osoba (IdMjesto, Ime, Prezime, Adresa, BrojTelefona) VALUES
    (1,'Zagorka','Savić', 'Trg Krajine 36', '065333965');

-- Clan
insert into Clan (IdClan, DatumUclanjivanja, DatumObnavljanjaClanstva, Email) VALUES
    (1,'2010-08-07','2021-08-07','jana.markovic@outlook.com');

-- Bibliotekar
insert into Bibliotekar (IdBibliotekar, KorisnickoIme, Lozinka) VALUES
    (2,'jovan123','jovan123');

-- Administrator
insert into Administrator (IdAdministrator, KorisnickoIme, Lozinka) VALUES
    (3,'zagorka123','zagorka123');

-- Pozajmica
insert into Pozajmica (IdClan, IdKnjiga, IdBibliotekar, DatumPozajmljivanja, Razduzena, Opis) VALUES
    (1,1,2,CURRENT_DATE,false, 'Knjiga je u savršenom stanju i neoštećena');


