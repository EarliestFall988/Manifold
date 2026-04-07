import { Link } from "@tanstack/react-router";
import { House, CloudSun, UsersIcon, MountainsIcon } from "@phosphor-icons/react";
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarGroup,
  SidebarGroupContent,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarHeader,
} from "@/components/ui/sidebar";
import { ThemeToggle } from "@/components/theme-toggle";

const navItems = [
  { title: "Home", to: "/", icon: House },
  { title: "Weather", to: "/weather/", icon: CloudSun },
  { title: "Students", to: "/student-list/", icon: UsersIcon },
  { title: "Course Catalog", to: "/course-catalog/", icon: MountainsIcon },
];

export function AppSidebar() {
  return (
    <Sidebar>
      <SidebarHeader className="p-4 font-heading font-semibold text-lg">
        App
      </SidebarHeader>
      <SidebarContent>
        <SidebarGroup>
          <SidebarGroupContent>
            <SidebarMenu>
              {navItems.map((item) => (
                <SidebarMenuItem key={item.to}>
                  <SidebarMenuButton asChild>
                    <Link to={item.to}>
                      <item.icon />
                      <span>{item.title}</span>
                    </Link>
                  </SidebarMenuButton>
                </SidebarMenuItem>
              ))}
            </SidebarMenu>
          </SidebarGroupContent>
        </SidebarGroup>
      </SidebarContent>
      <SidebarFooter className="p-2">
        <ThemeToggle />
      </SidebarFooter>
    </Sidebar>
  );
}
