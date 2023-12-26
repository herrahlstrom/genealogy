using System.ComponentModel.DataAnnotations;

namespace Genealogy.Domain.Entities;

public enum EventType
{
    None = 0,

    [Display(Name = "Födelse")] Födelse = 1,
    [Display(Name = "Död")] Död = 2,
    [Display(Name = "Döpt")] Döpt = 3,
    [Display(Name = "Boende")] Boende = 4,
    [Display(Name = "Boupptäckning")] Boupptäckning = 5,
    [Display(Name = "Begravd")] Begravd = 6,
    [Display(Name = "Övrigt")] Övrigt = 7,
    [Display(Name = "Yrke")] Yrke = 8,
    [Display(Name = "Förlovad")] Förlovad = 9,
    [Display(Name = "Gift")] Gift = 10,
    [Display(Name = "Skiljd")] Skiljd = 11,
    [Display(Name = "Separerad")] Separerad = 12,
    [Display(Name = "Förälder")] Förälder = 13,
    [Display(Name = "Flytt")] Flytt = 14,
    [Display(Name = "Dömd")] Dömd = 15,
}

public enum FamilyMemberType
{
    None = 0,

    [Display(Name = "Far")] Husband = 1,
    [Display(Name = "Mor")] Wife = 2,
    [Display(Name = "Barn")] Child = 3,
    [Display(Name = "Fosterbarn")] FosterChild = 4
}

public enum SourceType
{
    None = 0,

    [Display(Name = "Annan källa")]
    Other = 1,

    [Display(Name = "Riksarkivet")]
    Riksarkivet = 2,

    [Display(Name = "Arkiv Digital")]
    ArkivDigital = 3
}

public enum MediaType
{
    Unknown = 0,
    Potrait = 1,
    Source = 2
}