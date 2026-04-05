import { Outlet, createRootRoute } from "@tanstack/react-router";
import { ThemeProvider } from "@/lib/theme";
import { TooltipProvider } from "@/components/ui/tooltip";
import { SidebarProvider, SidebarTrigger } from "@/components/ui/sidebar";
import { AppSidebar } from "@/components/app-sidebar";

export const Route = createRootRoute({
  component: () => (
    <ThemeProvider>
      <TooltipProvider>
        <SidebarProvider>
          <AppSidebar />
          <main className="flex flex-col flex-1 min-w-0">
            <div className="px-2 overflow-auto h-[98%]">
              <header className="flex items-center gap-2 h-8 shrink-0">
                <SidebarTrigger />
              </header>
              <div className="flex-1 p-1 h-[95%] rounded-lg border-border border shadow-sm">
                <Outlet />
              </div>
            </div>
          </main>
        </SidebarProvider>
      </TooltipProvider>
    </ThemeProvider>
  ),
});
