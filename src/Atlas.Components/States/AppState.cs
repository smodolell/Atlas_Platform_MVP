using Atlas.Shared.Navegation;
using BitzArt.Blazor.Cookies;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;
using System.Text.Json;

namespace Atlas.Components.States;

public class AppState
{
    private readonly NavigationManager _navigation;
    private readonly ICookieService _cookieService;
    public string UserName { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;

    public void SetUser(string userName, string fullName)
    {
        UserName = userName;
        FullName = fullName;
        NotifyStateChanged();
    }
    public AppState(NavigationManager navigation, ICookieService cookieService)
    {
        _navigation = navigation;
        _cookieService = cookieService;

        SetupBaseTheme();
    }

    // =============================
    // EVENTO GLOBAL
    // =============================
    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();

    // =============================
    // NAVIGATION / LAYOUT
    // =============================

    public HashSet<AccessPointDto>? NavMenuItems { get; private set; }

    public bool IsNavOpen { get; set; } =true;

    public void SetMenu(HashSet<AccessPointDto> menu)
    {
        NavMenuItems = menu;
        NotifyStateChanged();
    }

    public void ToggleNav()
    {
        IsNavOpen = !IsNavOpen;
        NotifyStateChanged();
    }

    public void NavTo(AccessPointDto item)
    {
        if (!string.IsNullOrWhiteSpace(item.Route))
        {
            _navigation.NavigateTo(item.Route);
        }
    }

    // =============================
    // THEME
    // =============================

    private bool _isDark;
    private MudTheme _theme = new();

    public bool IsDark
    {
        get => _isDark;
        set
        {
            if (_isDark == value) return;

            _isDark = value;
            _ = SetCookieAsync("IsDark", JsonSerializer.Serialize(value));
            NotifyStateChanged();
        }
    }

    public MudColor PrimaryColor
    {
        get => _theme.PaletteLight.Primary;
        set
        {
            if (_theme.PaletteLight.Primary == value) return;

            UpdatePaletteColor(value);
            _ = SetCookieAsync("PrimaryColor", value.Value);
            NotifyStateChanged();
        }
    }

    public MudTheme MudTheme => _theme;
    private bool _initialized;
    public async Task InitializeAsync()
    {
        if (_initialized) return;
        _initialized = true;

        var isDarkCookie = await _cookieService.GetAsync("IsDark");
        bool.TryParse(isDarkCookie?.Value, out _isDark);

        var primaryColorCookie = await _cookieService.GetAsync("PrimaryColor");

        var primaryColor =
            !string.IsNullOrWhiteSpace(primaryColorCookie?.Value)
                ? primaryColorCookie.Value
                : "#1668dc";

        UpdatePaletteColor(new MudColor(primaryColor));

        NotifyStateChanged();
    }

    private void SetupBaseTheme()
    {
        _theme = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = "#1668dc",
                Secondary = "#00a2ae",
                Background = "#f8fafc",
                Surface = "#ffffff",
                AppbarBackground = "#ffffff",
                DrawerBackground = "#ffffff",
                TextPrimary = "#1e293b",
            },
            PaletteDark = new PaletteDark()
            {
                Primary = "#1668dc",
                Secondary = "#00a2ae",
                Background = "#121212",
                Surface = "#1e1e1e",
                AppbarBackground = "#1e1e1e",
                DrawerBackground = "#1e1e1e",
                TextPrimary = "#ffffff"
            }
        };
    }

    private void UpdatePaletteColor(MudColor color)
    {
        _theme.PaletteLight.Primary = color;
        _theme.PaletteLight.PrimaryDarken = color.ColorRgbDarken().ToString(MudColorOutputFormats.RGB);
        _theme.PaletteLight.PrimaryLighten = color.ColorRgbLighten().ToString(MudColorOutputFormats.RGB);

        _theme.PaletteDark.Primary = color;
        _theme.PaletteDark.PrimaryDarken = color.ColorRgbDarken().ToString(MudColorOutputFormats.RGB);
        _theme.PaletteDark.PrimaryLighten = color.ColorRgbLighten().ToString(MudColorOutputFormats.RGB);
    }

    private async Task SetCookieAsync(string key, string value)
    {
        await _cookieService.SetAsync(
            key,
            value,
            DateTimeOffset.Now.AddDays(30),
            httpOnly: false,
            secure: false,
            sameSiteMode: SameSiteMode.Lax
        );
    }
}