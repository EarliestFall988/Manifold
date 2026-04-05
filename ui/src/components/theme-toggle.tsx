import { Moon, Sun, Monitor } from "@phosphor-icons/react";
import { useTheme } from "@/lib/theme";
import { Button } from "@/components/ui/button";

export function ThemeToggle() {
  const { theme, setTheme } = useTheme();

  const next = theme === "light" ? "dark" : theme === "dark" ? "system" : "light";

  return (
    <Button variant="ghost" size="icon" onClick={() => setTheme(next)}>
      {theme === "light" && <Sun weight="bold" />}
      {theme === "dark" && <Moon weight="bold" />}
      {theme === "system" && <Monitor weight="bold" />}
    </Button>
  );
}
