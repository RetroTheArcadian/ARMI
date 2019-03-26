$Classes(ARMI.Models.*.*)[$Properties[]
export class $Name $ClassNameWithExtends {$Properties[
    $name: $Type;]
}]
${
    using Typewriter.Extensions.Types;
    string ClassNameWithExtends(Class c) {
        return (c.BaseClass!=null ?  " extends " + c.BaseClass.Name : string.Empty);
    }
    Template(Settings settings)
    {
        settings.IncludeCurrentProject();
        settings.OutputExtension = ".t4";
    }
}
