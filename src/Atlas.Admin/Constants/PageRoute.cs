namespace Atlas.Admin.Constants;

internal static class PageRoute
{
    public const string Prefix = "/";



    public const string UsuariosPage= Prefix + "usuarios";
    public const string AdminRoles = Prefix + "roles";
    public const string EmpresasPage = Prefix + "empresa";
    public const string SociosPage = Prefix + "socios";
    public const string NewSocioPage = Prefix + "new_socios";

    public const string ProductosPage = Prefix + "productos";
    public const string NewProductoPage = Prefix + "productos/nuevo";
    public const string EditProductoPage = Prefix + "productos/editar"; // ID added manually on routing

}
