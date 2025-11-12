public class Contact //Ansvarar för kontaktens data.
{
    //Properties
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
    public string Postcode { get; set; }
    public string City { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    //Konstruktor med namn, adress osv.
    public Contact(
        string name,
        string surname,
        string address,
        string postcode,
        string city,
        string phone,
        string email
    )
    {
        Name = name;
        Surname = surname;
        Address = address;
        Postcode = postcode;
        City = city;
        Phone = phone;
        Email = email;
    }

    //Egen ToString() för att enkelt kunna spara objektets information till fil i ett tydligt och läsbart textformat.
    public override string ToString()
    {
        return $"{Name},{Surname},{Address},{Postcode},{City},{Phone},{Email}";
    }

    //Metod för att visa kontakten i konsolen
    public string ToDisplay()
    {
        return $"Namn: {Name} {Surname}\nAdress: {Address}, {Postcode}, {City}\nTelefon: {Phone}\nE-mail: {Email} \n";
    }
}


