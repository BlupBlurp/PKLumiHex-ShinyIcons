using System;
using System.Collections.Generic;
using static PKHeX.Core.Species;

namespace PKHeX.Core;

/// <summary>
/// Contains logic for Alternate Form information.
/// </summary>
public static class FormInfo
{
    /// <summary>
    /// Checks if the form cannot exist outside of a Battle.
    /// </summary>
    /// <param name="species">Entity species</param>
    /// <param name="form">Entity form</param>
    /// <param name="format">Current generation format</param>
    /// <returns>True if it can only exist in a battle, false if it can exist outside of battle.</returns>
    public static bool IsBattleOnlyForm(ushort species, byte form, int format)
    {
        if (!BattleOnly.Contains(species))
            return false;

        // Some species have battle only forms as well as out-of-battle forms (other than base form).
        switch (species)
        {
            case (int)Slowbro when form == 2 && format >= 8: // this one is OK, Galarian Slowbro (not a Mega)
            case (int)Darmanitan when form == 2 && format >= 8: // this one is OK, Galarian non-Zen
            case (int)Zygarde when form < 4: // Zygarde Complete
            case (int)Mimikyu when form == 2: // Totem disguise Mimikyu
            case (int)Necrozma when form < 3: // Only mark Ultra Necrozma as Battle Only
                return false;
            case (int)Minior: return form < 7; // Minior Shields-Down

            default:
                return form != 0;
        }
    }

    /// <summary>
    /// Reverts the Battle Form to the form it would have outside of Battle.
    /// </summary>
    /// <remarks>Only call this if you've already checked that <see cref="IsBattleOnlyForm"/> returns true.</remarks>
    /// <param name="species">Entity species</param>
    /// <param name="form">Entity form</param>
    /// <param name="format">Current generation format</param>
    /// <returns>Suggested alt form value.</returns>
    public static byte GetOutOfBattleForm(ushort species, byte form, int format) => species switch
    {
        (int)Darmanitan => (byte)(form & 2),
        (int)Zygarde when format > 6 => 3,
        (int)Minior => (byte)(form + 7),
        _ => 0,
    };

    /// <summary>
    /// Indicates if the entity should be prevented from being traded away.
    /// </summary>
    /// <param name="species">Entity species</param>
    /// <param name="form">Entity form</param>
    /// <param name="formArg">Entity form argument</param>
    /// <param name="format">Current generation format</param>
    /// <returns>True if it trading should be disallowed.</returns>
    public static bool IsUntradable(ushort species, byte form, uint formArg, int format) => species switch
    {
        (int)Koraidon or (int)Miraidon when formArg == 1 => true, // Ride-able Box Legend
        (int)Pikachu when form == 8 && format == 7 => true, // Let's Go Pikachu Starter
        (int)Eevee when form == 1 && format == 7 => true, // Let's Go Eevee Starter
        _ => IsFusedForm(species, form, format),
    };

    /// <summary>
    /// Checks if the <see cref="form"/> is a fused form, which indicates it cannot be traded away.
    /// </summary>
    /// <param name="species">Entity species</param>
    /// <param name="form">Entity form</param>
    /// <param name="format">Current generation format</param>
    /// <returns>True if it is a fused species-form, false if it is not fused.</returns>
    public static bool IsFusedForm(ushort species, byte form, int format) => species switch
    {
        (int)Kyurem when form != 0 && format >= 5 => true,
        (int)Necrozma when form != 0 && format >= 7 => true,
        (int)Calyrex when form != 0 && format >= 8 => true,
        _ => false,
    };

    /// <summary>Checks if the form may be different than the original encounter detail.</summary>
    /// <param name="species">Original species</param>
    /// <param name="oldForm">Original form</param>
    /// <param name="newForm">Current form</param>
    /// <param name="origin">Encounter context</param>
    /// <param name="current">Current context</param>
    public static bool IsFormChangeable(ushort species, byte oldForm, byte newForm, EntityContext origin, EntityContext current)
    {
        if (FormChange.Contains(species))
            return true;

        // Zygarde Form Changing
        // Gen6: Introduced; no form changing.
        // Gen7: Form changing introduced; can only change to Form 2/3 (Power Construct), never to 0/1 (Aura Break). A form-1 can be boosted to form-0.
        // Gen8: Form changing improved; can pick any Form & Ability combination.
        if (species == (int)Zygarde)
        {
            return current switch
            {
                EntityContext.Gen6 => false,
                EntityContext.Gen7 => newForm >= 2 || (oldForm == 1 && newForm == 0),
                _ => true,
            };
        }
        if (species is (int)Deerling or (int)Sawsbuck)
        {
            return origin switch
            {
                EntityContext.Gen5 => true,
                EntityContext.Gen9 => true,
                _ => false, // todo home sv
            };
        }
        return false;
    }

    public static bool IsFormChangeEgg(ushort species) => FormChangeEgg.Contains(species);

    /// <summary>
    /// Species forms that cannot be found in Luminescent Platinum 2.0F.
    /// </summary>
    /// <remarks>Only includes those introduced before gen 5</remarks>
    public static byte[] Unavailable(ushort species) => species switch
    {
        (ushort)Butterfree or (ushort)Beedrill or (ushort)Pidgeot or (ushort)Alakazam or (ushort)Machamp or (ushort)Slowbro
        or (ushort)Dewgong or (ushort)Kingler or (ushort)Kangaskhan or (ushort)Pinsir or (ushort)Gyarados or (ushort)Lapras
        or (ushort)Aerodactyl or (ushort)Snorlax
        or (ushort)Ampharos or (ushort)Wooper or (ushort)Steelix or (ushort)Scizor or (ushort)Heracross or (ushort)Houndoom or (ushort)Tyranitar
        or (ushort)Sceptile or (ushort)Blaziken or (ushort)Swampert or (ushort)Gardevoir or (ushort)Sableye or (ushort)Mawile
        or (ushort)Aggron or (ushort)Medicham or (ushort)Manectric or (ushort)Sharpedo or (ushort)Camerupt or (ushort)Altaria
        or (ushort)Banette or (ushort)Absol or (ushort)Glalie or (ushort)Salamence or (ushort)Metagross
        or (ushort)Latias or (ushort)Latios or (ushort)Kyogre or (ushort)Groudon or (ushort)Rayquaza
        or (ushort)Lopunny or (ushort)Garchomp or (ushort)Lucario or (ushort)Abomasnow or (ushort)Gallade
        or (ushort)Dialga or (ushort)Palkia => (new byte[] { 1 }),

        (ushort)Raticate or (ushort)Marowak or (ushort)Eevee => (new byte[] { 2 }),

        (ushort)Meowth => (new byte[] { 3 }),

        (ushort)Venusaur or (ushort)Blastoise or (ushort)Gengar or (ushort)Mewtwo => (new byte[] {1, 2}),

        (ushort)Charizard or (ushort)Tauros => (new byte[] {1, 2, 3}),

        (ushort)Pikachu => (new byte[] {7, 8, 9, 10, 11, 12, 13, 14, 15, 16}),

        _ => ((Array.Empty<byte>()))
    };

    /// <summary>
    /// Species that can be found in Luminescent Platinum 2.0F.
    /// </summary>
    /// <remarks>Only includes those introduced beyond gen 4</remarks>
    public static readonly HashSet<ushort> NewGenAvailables = new()
    {
        (ushort)Sylveon,
        (ushort)Perrserker, (ushort)Sirfetchd, (ushort)MrRime, (ushort)Cursola, (ushort)Obstagoon,
        (ushort)Kleavor, (ushort)Overqwil, (ushort)Sneasler, (ushort)Wyrdeer, (ushort)Ursaluna
    };

    /// <summary>
    /// Species with forms that cannot be found in Luminescent Platinum 2.0F.
    /// </summary>
    /// <remarks>Only includes those introduced before gen 5</remarks>
    public static readonly HashSet<ushort> WithUnavailableForm = new()
    {
        (ushort)Venusaur, (ushort)Charizard, (ushort)Blastoise, (ushort)Alakazam, (ushort)Gengar,
        (ushort)Kangaskhan, (ushort)Pinsir, (ushort)Gyarados, (ushort)Aerodactyl, (ushort)Mewtwo,
        (ushort)Beedrill, (ushort)Pidgeot, (ushort)Slowbro,
        (ushort)Raticate, (ushort)Marowak,
        (ushort)Butterfree, (ushort)Pikachu, (ushort)Meowth, (ushort)Machamp, (ushort)Kingler,
        (ushort)Lapras, (ushort)Eevee, (ushort)Snorlax,
        (ushort)Tauros,
        (ushort)Dewgong,

        (ushort)Ampharos, (ushort)Scizor, (ushort)Heracross, (ushort)Houndoom, (ushort)Tyranitar,
        (ushort)Steelix,
        (ushort)Wooper,

        (ushort)Blaziken, (ushort)Gardevoir, (ushort)Mawile, (ushort)Aggron, (ushort)Medicham,
        (ushort)Manectric, (ushort)Banette, (ushort)Absol, (ushort)Latias, (ushort)Latios,
        (ushort)Sceptile, (ushort)Swampert, (ushort)Sableye, (ushort)Sharpedo, (ushort)Camerupt,
        (ushort)Altaria, (ushort)Glalie, (ushort)Salamence, (ushort)Metagross, 
        (ushort)Groudon, (ushort)Kyogre, (ushort)Rayquaza,

        (ushort)Garchomp, (ushort)Lucario, (ushort)Abomasnow,
        (ushort)Lopunny, (ushort)Gallade,
        (ushort)Dialga, (ushort)Palkia,
    };

    private static ReadOnlySpan<ushort> FormChangeEgg => new ushort[]
    {
        (int)Burmy,
        (int)Furfrou,
        (int)Oricorio,
    };

    /// <summary>
    /// Species that can change between their forms, regardless of origin.
    /// </summary>
    /// <remarks>Excludes Zygarde as it has special conditions. Check separately.</remarks>
    private static readonly HashSet<ushort> FormChange = new()
    {
        (int)Burmy,
        (int)Furfrou,
        (int)Oricorio,

        // Sometimes considered for wild encounters
        (int)Rotom,

        (int)Deoxys,
        (int)Dialga,
        (int)Palkia,
        (int)Giratina,
        (int)Shaymin,
        (int)Arceus,
        (int)Tornadus,
        (int)Thundurus,
        (int)Landorus,
        (int)Kyurem,
        (int)Keldeo,
        (int)Genesect,
        (int)Hoopa,
        (int)Silvally,
        (int)Necrozma,
        (int)Calyrex,
        (int)Enamorus,
    };

    /// <summary>
    /// Species that have an alternate form that cannot exist outside of battle.
    /// </summary>
    private static readonly HashSet<ushort> BattleForms = new()
    {
        (int)Castform,
        (int)Cherrim,
        (int)Darmanitan,
        (int)Meloetta,
        (int)Aegislash,
        (int)Xerneas,
        (int)Zygarde,

        (int)Wishiwashi,
        (int)Minior,
        (int)Mimikyu,

        (int)Cramorant,
        (int)Morpeko,
        (int)Eiscue,

        (int)Zacian,
        (int)Zamazenta,
        (int)Eternatus,

        (int)Palafin,
    };

    /// <summary>
    /// Species that have a mega form that cannot exist outside of battle.
    /// </summary>
    /// <remarks>Using a held item to change form during battle, via an in-battle transformation feature.</remarks>
    private static readonly HashSet<ushort> BattleMegas = new()
    {
        // XY
        (int)Venusaur, (int)Charizard, (int)Blastoise,
        (int)Alakazam, (int)Gengar, (int)Kangaskhan, (int)Pinsir,
        (int)Gyarados, (int)Aerodactyl, (int)Mewtwo,

        (int)Ampharos, (int)Scizor, (int)Heracross, (int)Houndoom, (int)Tyranitar,

        (int)Blaziken, (int)Gardevoir, (int)Mawile, (int)Aggron, (int)Medicham,
        (int)Manectric, (int)Banette, (int)Absol, (int)Latios, (int)Latias,

        (int)Garchomp, (int)Lucario, (int)Abomasnow,

        // AO
        (int)Beedrill, (int)Pidgeot, (int)Slowbro,

        (int)Steelix,

        (int)Sceptile, (int)Swampert, (int)Sableye, (int)Sharpedo, (int)Camerupt,
        (int)Altaria, (int)Glalie, (int)Salamence, (int)Metagross, (int)Rayquaza,

        (int)Lopunny, (int)Gallade,
        (int)Audino, (int)Diancie,

        // USUM
        (int)Necrozma, // Ultra Necrozma
    };

    /// <summary>
    /// Species that have a primal form that cannot exist outside of battle.
    /// </summary>
    private static readonly HashSet<ushort> BattlePrimals = new() { (int)Kyogre, (int)Groudon };

    private static readonly HashSet<ushort> BattleOnly = GetBattleFormSet();

    private static HashSet<ushort> GetBattleFormSet()
    {
        var hs = new HashSet<ushort>(BattleForms);
        hs.UnionWith(BattleMegas);
        hs.UnionWith(BattlePrimals);
        return hs;
    }

    /// <summary>
    /// Species has a Totem form in Gen7 (S/M &amp; US/UM) that can be captured and owned.
    /// </summary>
    /// <param name="species"></param>
    /// <returns>True if the species exists as a Totem.</returns>
    /// <remarks>Excludes <see cref="Wishiwashi"/> because it cannot be captured.</remarks>
    public static bool HasTotemForm(ushort species) => species switch
    {
        (ushort)Raticate => true,
        (ushort)Marowak => true,
        (ushort)Gumshoos => true,
        (ushort)Vikavolt => true,
        (ushort)Ribombee => true,
        (ushort)Araquanid => true,
        (ushort)Lurantis => true,
        (ushort)Salazzle => true,
        (ushort)Mimikyu => true,
        (ushort)Kommoo => true,
        (ushort)Togedemaru => true,
        _ => false,
    };

    /// <summary>
    /// Checks if the <see cref="form"/> for the <see cref="species"/> is a Totem form.
    /// </summary>
    /// <param name="species">Entity species</param>
    /// <param name="form">Entity form</param>
    /// <param name="context">Current generation format</param>
    public static bool IsTotemForm(ushort species, byte form, EntityContext context) => context == EntityContext.Gen7 && IsTotemForm(species, form);

    /// <summary>
    /// Checks if the <see cref="form"/> for the <see cref="species"/> is a Totem form.
    /// </summary>
    /// <remarks>Use <see cref="IsTotemForm(ushort,byte,EntityContext)"/> if you aren't 100% sure the format is 7.</remarks>
    /// <param name="species">Entity species</param>
    /// <param name="form">Entity form</param>
    public static bool IsTotemForm(ushort species, byte form)
    {
        if (form == 0)
            return false;
        if (!HasTotemForm(species))
            return false;
        if (species == (int)Mimikyu)
            return form is 2 or 3;
        if (species is (int)Raticate or (int)Marowak)
            return form == 2;
        return form == 1;
    }

    /// <summary>
    /// Gets the base <see cref="form"/> for the <see cref="species"/> when the Totem form is reverted (on transfer).
    /// </summary>
    /// <param name="species">Entity species</param>
    /// <param name="form">Entity form</param>
    public static byte GetTotemBaseForm(ushort species, byte form)
    {
        if (species == (int)Mimikyu)
            return 0;
        return --form;
    }

    public static bool IsLordForm(ushort species, byte form, EntityContext context)
    {
        if (context != EntityContext.Gen8a)
            return false;
        return IsLordForm(species, form);
    }

    private static bool IsLordForm(ushort species, byte form) => form != 0 && species switch
    {
        (int)Arcanine when form == 2 => true,
        (int)Electrode when form == 2 => true,
        (int)Lilligant when form == 2 => true,
        (int)Avalugg when form == 2 => true,
        (int)Kleavor when form == 1 => true,
        _ => false,
    };

    /// <summary>
    /// Checks if the <see cref="form"/> exists for the <see cref="species"/> without having an associated <see cref="PersonalInfo"/> index.
    /// </summary>
    /// <param name="species">Entity species</param>
    /// <param name="form">Entity form</param>
    /// <param name="format">Current generation format</param>
    /// <seealso cref="HasFormValuesNotIndicatedByPersonal"/>
    public static bool IsValidOutOfBoundsForm(ushort species, byte form, int format) => (Species) species switch
    {
        Unown => form < (format == 2 ? 26 : 28), // A-Z : A-Z?!
        Mothim => form < 3, // Burmy base form is kept

        Scatterbug => form <= Vivillon3DS.MaxWildFormID, // Vivillon Pre-evolutions
        Spewpa => form <= Vivillon3DS.MaxWildFormID, // Vivillon Pre-evolutions

        _ => false,
    };

    /// <summary>
    /// Checks if the <see cref="PKM"/> data should have a drop-down selection visible for the <see cref="PKM.Form"/> value.
    /// </summary>
    /// <param name="pi">Game specific personal info</param>
    /// <param name="species"><see cref="Species"/> ID</param>
    /// <param name="format"><see cref="PKM.Form"/> ID</param>
    /// <returns>True if has forms that can be provided by <see cref="FormConverter.GetFormList"/>, otherwise false for none.</returns>
    public static bool HasFormSelection(IPersonalFormInfo pi, ushort species, int format)
    {
        if (format <= 3 && species != (int)Unown)
            return false;

        if (HasFormValuesNotIndicatedByPersonal(species))
            return true;

        int count = pi.FormCount;
        return count > 1;
    }

    /// <summary>
    /// <seealso cref="IsValidOutOfBoundsForm"/>
    /// </summary>
    private static bool HasFormValuesNotIndicatedByPersonal(ushort species) => species switch
    {
        (int)Unown => true,
        (int)Mothim => true, // (Burmy form is not cleared on evolution)
        (int)Scatterbug or (int)Spewpa => true, // Vivillon pre-evos
        _ => false,
    };
}
