use biblioteka;

-- Mjesto
insert into Mjesto (Naziv, PostanskiBroj) VALUES ('Banja Luka', '78000');
insert into Mjesto (Naziv, PostanskiBroj) VALUES ('Doboj','78200');
insert into Mjesto (Naziv, PostanskiBroj) VALUES ('Trebinje', '78900');
insert into Mjesto (Naziv, PostanskiBroj) VALUES ('Bijeljina','78500');
insert into Mjesto (Naziv, PostanskiBroj) VALUES ('Istočno Sarajevo','78100');
insert into Mjesto (Naziv, PostanskiBroj) VALUES ('Moskva','36857');
insert into Mjesto (Naziv, PostanskiBroj) VALUES ('Beograd','25875');

-- Zanr
insert into Zanr (Naziv, Opis) VALUES ('horor','Samo za one sa jakim srcem...');
insert into Zanr (Naziv, Opis) VALUES ('psihologija','Tajne ljudske duše i uma...');
insert into Zanr (Naziv, Opis) VALUES ('poezija','Najljepši stiovi poezije...');
insert into Zanr (Naziv, Opis) VALUES ('istorija','Priče o minulim vremenima...');
insert into Zanr (Naziv, Opis) VALUES ('klasična književnost','Samo za odabrane ljubitelje knjiga...');

-- Autor
insert into Autor (IdMjesto, Ime, Prezime, DatumRodjenja) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Banja Luka'),'Petar', 'Kočić','1877-04-09');
insert into Autor (IdMjesto, Ime, Prezime, DatumRodjenja) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Doboj'),'Marko', 'Maran','1975-07-05');
insert into Autor (IdMjesto, Ime, Prezime, DatumRodjenja) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Trebinje'),'Zlatko', 'Vukota','1965-05-03');
insert into Autor (IdMjesto, Ime, Prezime, DatumRodjenja) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Bijeljina'),'Ana', 'Ris','1990-01-02');
insert into Autor (IdMjesto, Ime, Prezime, DatumRodjenja) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Istočno Sarajevo'),'Marija', 'Blažić','1980-04-06');
insert into Autor (IdMjesto, Ime, Prezime, DatumRodjenja) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Moskva'),'Fjodor', 'M. Dostojevski','1870-04-06');
insert into Autor (IdMjesto, Ime, Prezime, DatumRodjenja) VALUES
    ((select IdMjesto from Mjesto where Naziv = 'Beograd'),'Desanka', 'Maksimović','1910-04-06');

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
insert into Knjiga (NazivZanra,IdAutor, IdIzdavac, Naslov, DatumObjavljivanja, ISBN, UkupanBrojKopija, BrojStranica, Jezik, Opis) VALUES
    ('klasična književnost',1 , 1,'Jazavac pred sudom', '1870-05-07','546512321548456548754565452154', 100, 80, 'srpski', 'Jazavac pred sudom je...');
insert into Knjiga (NazivZanra,IdAutor, IdIzdavac, Naslov, DatumObjavljivanja, ISBN, UkupanBrojKopija, BrojStranica, Jezik, Opis) VALUES
    ('horor',2,2,'Smrt u Veneciji', '2010-08-07','546512321548456548754561252154', 80, 300, 'srpski', 'Smrt u Veneciji je...');
insert into Knjiga (NazivZanra,IdAutor, IdIzdavac, Naslov, DatumObjavljivanja, ISBN, UkupanBrojKopija, BrojStranica, Jezik, Opis) VALUES
    ('klasična književnost',6,3,'Braća Karamazovi', '1850-08-07','546512321548453698754561252154', 250, 1300, 'srpski', 'Kapitalno djelo slavnog pisca...');
insert into Knjiga (NazivZanra,IdAutor, IdIzdavac, Naslov, DatumObjavljivanja, ISBN, UkupanBrojKopija, BrojStranica, Jezik, Opis) VALUES
    ('klasična književnost',6,3,'Zločin i kazna', '1850-08-07','546512321167456888754561252154', 250, 1300, 'srpski', 'Kapitalno djelo slavnog pisca...');
insert into Knjiga (NazivZanra,IdAutor, IdIzdavac, Naslov, DatumObjavljivanja, ISBN, UkupanBrojKopija, BrojStranica, Jezik, Opis) VALUES
    ('klasična književnost',6,3,'Kockar', '1850-08-07','546512321548147888754561252154', 250, 1300, 'srpski', 'Kapitalno djelo slavnog pisca...');
insert into Knjiga (NazivZanra,IdAutor, IdIzdavac, Naslov, DatumObjavljivanja, ISBN, UkupanBrojKopija, BrojStranica, Jezik, Opis) VALUES
    ('poezija',7,4,'Tražim pomilovanje', '1955-08-07','546512321333456369754561252154', 350, 150, 'srpski', 'Djelo protkano...');
insert into Knjiga (NazivZanra,IdAutor, IdIzdavac, Naslov, DatumObjavljivanja, ISBN, UkupanBrojKopija, BrojStranica, Jezik, Opis) VALUES
    ('poezija',7,4,'Zeleni vitez', '1955-08-07','546512321333456548725861252154', 350, 150, 'srpski', 'Djelo protkano...');
insert into Knjiga (NazivZanra,IdAutor, IdIzdavac, Naslov, DatumObjavljivanja, ISBN, UkupanBrojKopija, BrojStranica, Jezik, Opis) VALUES
    ('istorija',3,4,'Julije Cezar - Biografija', '1995-08-07','546512321312556548754561252154', 80, 150, 'srpski', 'Djelo protkano...');

-- Osoba
insert into Osoba (NazivMjesta, Ime, Prezime, Adresa, BrojTelefona, Email, DatumRodjenja) VALUES
    ('Banja Luka','Jana','Maran', 'Trg Krajine 5', '065658965','jana@gmail.com','1990-05-07');
insert into Osoba (NazivMjesta, Ime, Prezime, Adresa, BrojTelefona, Email, DatumRodjenja) VALUES
    ('Banja Luka','Jovan','Marković', 'Trg Krajine 25', '065657775','jovan@gmail.com','1995-08-08');
insert into Osoba (NazivMjesta, Ime, Prezime, Adresa, BrojTelefona, Email, DatumRodjenja) VALUES
    ('Banja Luka','Zagorka','Savić', 'Trg Krajine 36', '065333965', 'zaga@gmail.com','1995-07-07');

-- Clan
insert into Clan (IdClan, DatumUclanjivanja, DatumObnavljanjaClanstva) VALUES
    (3,'2010-08-07','2021-08-07');

-- Bibliotekar
insert into Bibliotekar (IdBibliotekar, KorisnickoIme, Lozinka) VALUES
    (2,'jovan123','jovan123');
insert into Bibliotekar (IdBibliotekar, KorisnickoIme, Lozinka) VALUES
    (1,'admin','admin');
    
-- Tema
insert into Tema(IdOsoba, Stil) values (2,1);
insert into Tema(IdOsoba, Stil) values (1,1);

-- Administrator
insert into Administrator (IdAdministrator, KorisnickoIme, Lozinka) VALUES
    (1,'admin','admin');

-- Pozajmica
insert into Pozajmica (IdClan, IdKnjiga, IdBibliotekar, DatumPozajmljivanja, JeRazduzena, Opis) VALUES
    (3,1,2,'2021-01-01',false, 'Knjiga je u savršenom stanju i neoštećena');

-- Obavjestenje
insert into Obavjestenje (IdAdministrator,IdBibliotekar, Naslov,Datum,Tekst,ZaSve) values
	(1,2,'Godisnji odmor','2021-01-01','Od 28.03.2021. Pocinje redovni godisnji odmor...',true);
insert into Obavjestenje (IdAdministrator,IdBibliotekar, Naslov,Datum,Tekst,ZaSve) values
	(1,2,'Bonus na platu','2021-01-01','Cestitamo vi ste radnik mjeseca, mozete preuzeti vasu nagradu od 150KM u kancelariji broj 15. kao i ...',false);


