namespace TJC.Logging.Settings.Components;

public class Inclusion(bool included = true)
{
    #region Fields

    private bool _included = included;

    #endregion

    #region Methods

    public void Include() =>
        _included = true;

    public void Exclude() =>
        _included = false;

    #endregion

    #region Operators

    public static implicit operator Inclusion(bool value) =>
        new(value);

    public static bool operator true(Inclusion inclusion) =>
        inclusion._included;

    public static bool operator false(Inclusion inclusion) =>
        !inclusion._included;

    public static bool operator !(Inclusion inclusion) =>
        !inclusion._included;

    public static Inclusion operator &(Inclusion x, Inclusion y) =>
        new(x._included & y._included);

    public static Inclusion operator |(Inclusion x, Inclusion y) =>
        new(x._included | y._included);

    #endregion
}