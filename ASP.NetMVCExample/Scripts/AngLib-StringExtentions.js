
String.prototype.Insert = function (Index, Value)
{
    var String = this;
    String = String.substr(0, Index) + Value + String.substr(Index, String.length - Index);
    return String;
};

String.prototype.FormatUArg = function ()
{
    var String = this,
        i = arguments.length;

    while (i--)
    {
        String = String.replace(new RegExp('\\{' + i + '\\}', 'gm'), arguments[i]);
    }
    return String;
};

String.prototype.FormatUOjb = function (placeholders)
{
    var String = this;
    for (var propertyName in placeholders)
    {
        String = String.replace(new RegExp('{' + propertyName + '}', 'gm'), placeholders[propertyName]);
    }
    return String;
};