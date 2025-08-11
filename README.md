# **ElementalWords**

A C# Console Application that takes one parameter `word`, searches a dictionary of chemical elements from the periodic table and returns the results.

**Example**
---
The word `snack` can be formed by concatenating the symbols of 3 different combinations of elements:

|               1               |               2               |               3               |
| ----------------------------- | ----------------------------- | ----------------------------- |
| Sulfur (S)                    | Sulfur (S)                    | Tin (Sn)                       |
| Nitrogen (N)                  | Sodium (Na)                   | Actinium (Ac)                  |
| Actinium (Ac)                 | Carbon (C)                    | Potassium (K)                  |
| Potassium (K)                 | Potassium (K)                  |                                |

**Notes**
---
- Supports element symbol length of between 1 and 3 characters.
- Input word matching ignores case.
- Includes pre-2016 temporary symbols for elements 113, 115, 117, and 118.