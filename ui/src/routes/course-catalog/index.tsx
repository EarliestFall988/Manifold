import { Header } from "@/components/header";
import { Loader } from "@/components/loader";
import { Button } from "@/components/ui/button";
import {
  InputGroup,
  InputGroupAddon,
  InputGroupInput,
} from "@/components/ui/input-group";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { useCourse } from "@/hooks/Course";
import { exportCsv } from "@/lib/exportCsv";
import {
  BookOpenIcon,
  DownloadSimpleIcon,
  MagnifyingGlassIcon,
  SpinnerIcon,
} from "@phosphor-icons/react";
import { createFileRoute, useNavigate } from "@tanstack/react-router";
import { useMemo, useState } from "react";

export const Route = createFileRoute("/course-catalog/")({
  component: RouteComponent,
});

function RouteComponent() {
  const [search, setSearch] = useState("");

  const filter = useMemo(() => `$filter=contains(tolower(Name),tolower('${search}'))`, [search]);

  const { data, isLoading } = useCourse(filter);
  const courses = useMemo(() => data?.value ?? [], [data]);

  const nav = useNavigate();

  const handleAddCourse = () => {
    nav({ to: "/course-catalog/add" });
  };

  return (
    <div>
      <Header
        headerText="Course Catalog"
        description="Browse and manage available courses"
      />

      <div className="flex gap-4 p-2 items-center justify-center">
        <InputGroup>
          <InputGroupInput
            placeholder="Search..."
            value={search}
            onChange={(e) => setSearch(e.target.value)}
          />
          <InputGroupAddon>
            <MagnifyingGlassIcon />
          </InputGroupAddon>
        </InputGroup>

        <Button variant={"outline"} onClick={handleAddCourse}>
          <BookOpenIcon />
          New Course
        </Button>

        <Button
          variant={"outline"}
          disabled={courses.length === 0}
          onClick={() =>
            exportCsv(
              "courses.csv",
              courses.map((c) => ({
                Id: c.Id,
                Name: c.Name,
                Description: c.Description,
                Weeks: c.Weeks,
                Credits: c.Credits,
              })),
            )
          }
        >
          <DownloadSimpleIcon />
          Export CSV
        </Button>
      </div>
      <div className="p-2">
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Name</TableHead>
              <TableHead>Description</TableHead>
              <TableHead>Weeks</TableHead>
              <TableHead>Credits</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {isLoading ? (
              <TableRow>
                <TableCell
                  colSpan={4}
                  className="text-center text-muted-foreground"
                >
                    Loading...
                </TableCell>
              </TableRow>
            ) : courses.length === 0 ? (
              <TableRow>
                <TableCell
                  colSpan={4}
                  className="text-center text-muted-foreground"
                >
                  No courses found.
                </TableCell>
              </TableRow>
            ) : (
              courses.map((c) => (
                <TableRow
                  key={c.Id}
                  className="cursor-pointer"
                  onClick={() =>
                    nav({
                      to: "/course-catalog/$id",
                      params: { id: String(c.Id) },
                    })
                  }
                >
                  <TableCell>{c.Name}</TableCell>
                  <TableCell>{c.Description}</TableCell>
                  <TableCell>{c.Weeks}</TableCell>
                  <TableCell>{c.Credits}</TableCell>
                </TableRow>
              ))
            )}
          </TableBody>
        </Table>
      </div>
    </div>
  );
}
