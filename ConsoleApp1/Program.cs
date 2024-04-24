using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    private static Dictionary<string, double> atomMasses = new Dictionary<string, double>
    {
        ["H"] = 1.008,    // Hydrogen
        ["He"] = 4.0026,  // Helium
        ["Li"] = 6.94,    // Lithium
        ["Be"] = 9.0122,  // Beryllium
        ["B"] = 10.81,    // Boron
        ["C"] = 12.011,   // Carbon
        ["N"] = 14.007,   // Nitrogen
        ["O"] = 15.999,   // Oxygen
        ["F"] = 18.998,   // Fluorine
        ["Ne"] = 20.180,  // Neon
        ["Na"] = 22.990,  // Sodium
        ["Mg"] = 24.305,  // Magnesium
        ["Al"] = 26.982,  // Aluminum
        ["Si"] = 28.085,  // Silicon
        ["P"] = 30.974,   // Phosphorus
        ["S"] = 32.06,    // Sulfur
        ["Cl"] = 35.45,   // Chlorine
        ["Ar"] = 39.948,  // Argon
        ["K"] = 39.098,   // Potassium
        ["Ca"] = 40.078,  // Calcium
        ["Sc"] = 44.956,  // Scandium
        ["Ti"] = 47.867,  // Titanium
        ["V"] = 50.942,   // Vanadium
        ["Cr"] = 51.996,  // Chromium
        ["Mn"] = 54.938,  // Manganese
        ["Fe"] = 55.845,  // Iron
        ["Co"] = 58.933,  // Cobalt
        ["Ni"] = 58.693,  // Nickel
        ["Cu"] = 63.546,  // Copper
        ["Zn"] = 65.38,   // Zinc
        ["Ga"] = 69.723,  // Gallium
        ["Ge"] = 72.630,  // Germanium
        ["As"] = 74.922,  // Arsenic
        ["Se"] = 78.971,  // Selenium
        ["Br"] = 79.904,  // Bromine
        ["Kr"] = 83.798,  // Krypton
        ["Rb"] = 85.468,  // Rubidium
        ["Sr"] = 87.62,   // Strontium
        ["Y"] = 88.906,   // Yttrium
        ["Zr"] = 91.224,  // Zirconium
        ["Nb"] = 92.906,  // Niobium
        ["Mo"] = 95.95,   // Molybdenum
        ["Tc"] = 98,      // Technetium (most stable isotope)
        ["Ru"] = 101.07,  // Ruthenium
        ["Rh"] = 102.91,  // Rhodium
        ["Pd"] = 106.42,  // Palladium
        ["Ag"] = 107.87,  // Silver
        ["Cd"] = 112.41,  // Cadmium
        ["In"] = 114.82,  // Indium
        ["Sn"] = 118.71,  // Tin
        ["Sb"] = 121.76,  // Antimony
        ["Te"] = 127.60,  // Tellurium
        ["I"] = 126.90,   // Iodine
        ["Xe"] = 131.29,  // Xenon
        ["Cs"] = 132.91,  // Cesium
        ["Ba"] = 137.33,  // Barium
                          // Lanthanides
        ["La"] = 138.91,  // Lanthanum
        ["Ce"] = 140.12,  // Cerium
        ["Pr"] = 140.91,  // Praseodymium
        ["Nd"] = 144.24,  // Neodymium
        ["Pm"] = 145,     // Promethium (most stable isotope)
        ["Sm"] = 150.36,  // Samarium
        ["Eu"] = 151.96,  // Europium
        ["Gd"] = 157.25,  // Gadolinium
        ["Tb"] = 158.93,  // Terbium
        ["Dy"] = 162.50,  // Dysprosium
        ["Ho"] = 164.93,  // Holmium
        ["Er"] = 167.26,  // Erbium
        ["Tm"] = 168.93,  // Thulium
        ["Yb"] = 173.05,  // Ytterbium
        ["Lu"] = 174.97,  // Lutetium
                          // Actinides
        ["Th"] = 232.04,  // Thorium
        ["Pa"] = 231.04,  // Protactinium
        ["U"] = 238.03,   // Uranium
        ["Np"] = 237,     // Neptunium (most stable isotope)
        ["Pu"] = 244,     // Plutonium (most stable isotope)
        ["Am"] = 243,     // Americium (most stable isotope)
        ["Cm"] = 247,     // Curium (most stable isotope)
        ["Bk"] = 247,     // Berkelium (most stable isotope)
        ["Cf"] = 251,     // Californium (most stable isotope)
        ["Es"] = 252,     // Einsteinium (most stable isotope)
        ["Fm"] = 257,     // Fermium (most stable isotope)
        ["Md"] = 258,     // Mendelevium (most stable isotope)
        ["No"] = 259,     // Nobelium (most stable isotope)
        ["Lr"] = 266,     // Lawrencium (most stable isotope)
        ["Rf"] = 267,     // Rutherfordium
        ["Db"] = 270,     // Dubnium
        ["Sg"] = 271,     // Seaborgium
        ["Bh"] = 270,     // Bohrium
        ["Hs"] = 277,     // Hassium
        ["Mt"] = 276,     // Meitnerium
        ["Ds"] = 281,     // Darmstadtium
        ["Rg"] = 282,     // Roentgenium
        ["Cn"] = 285,     // Copernicium
        ["Nh"] = 286,     // Nihonium
        ["Fl"] = 289,     // Flerovium
        ["Mc"] = 290,     // Moscovium
        ["Lv"] = 293,     // Livermorium
        ["Ts"] = 294,     // Tennessine
        ["Og"] = 294      // Oganesson
    };

    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1) Calculate molecular weight");
            Console.WriteLine("2) Show all elements");
            Console.WriteLine("3) Exit");
            Console.Write("Select an option: ");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CalculateMolecularWeight();
                    break;
                case "2":
                    ShowAllElements();
                    break;
                case "3":
                    Console.WriteLine("Exiting the program.");
                    return;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }

    private static void CalculateMolecularWeight()
    {
        Console.Write("Enter the chemical formula: ");
        string formula = Console.ReadLine();
        try
        {
            double mass = CalculateMass(formula);
            Console.WriteLine($"The mass of the molecule for the formula {formula} is: {mass:F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    private static double CalculateMass(string formula)
    {
        Stack<double> stack = new Stack<double>();
        int i = 0;
        while (i < formula.Length)
        {
            if (char.IsLetter(formula[i]))
            {
                string element = formula[i].ToString();
                if (i + 1 < formula.Length && char.IsLower(formula[i + 1]))
                {
                    element += formula[i + 1]; // Capture multi-character element symbols like "He"
                    i++;
                }
                i++;
                int count = 1;
                if (i < formula.Length && char.IsDigit(formula[i])) // Improved digit parsing
                {
                    int startIndex = i;
                    while (i < formula.Length && char.IsDigit(formula[i])) // Handle multi-digit numbers
                    {
                        i++;
                    }
                    count = int.Parse(formula.Substring(startIndex, i - startIndex));
                }

                if (atomMasses.TryGetValue(element, out double elementMass))
                {
                    stack.Push(elementMass * count);
                }
                else
                {
                    throw new ArgumentException($"Unknown element: {element}");
                }
            }
            else if (formula[i] == '(')
            {
                stack.Push(-1); // Marker for start of a group
                i++;
            }
            else if (formula[i] == ')')
            {
                double groupMass = 0;
                while (stack.Peek() != -1)
                {
                    groupMass += stack.Pop();
                }
                stack.Pop(); // Remove the -1 marker
                i++;

                int multiplier = 1;
                if (i < formula.Length && char.IsDigit(formula[i])) // Handle multi-digit multipliers
                {
                    int startIndex = i;
                    while (i < formula.Length && char.IsDigit(formula[i]))
                    {
                        i++;
                    }
                    multiplier = int.Parse(formula.Substring(startIndex, i - startIndex));
                }

                stack.Push(groupMass * multiplier);
            }
        }

        double totalMass = 0;
        while (stack.Count > 0)
        {
            totalMass += stack.Pop();
        }

        return totalMass;
    }

    private static void ShowAllElements()
    {
        foreach (var element in atomMasses)
        {
            Console.WriteLine($"{element.Key}: {element.Value:F2}");
        }
    }
}