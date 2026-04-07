import { Header } from "@/components/header";
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
import { useStudent } from "@/hooks/Student";
import { MagnifyingGlassIcon, UserPlusIcon } from "@phosphor-icons/react";
import { createFileRoute, useNavigate } from "@tanstack/react-router";
import { useState } from "react";

export const Route = createFileRoute("/student-list/")({
  component: RouteComponent,
});

function RouteComponent() {
  const [search, setSearch] = useState("");
  const { data, isLoading } = useStudent();

  const students = (data?.value ?? []).filter((s) =>
    s.Name.toLowerCase().includes(search.toLowerCase()),
  );

  const nav = useNavigate();

  const handleAddStudent = () => {
    nav({ to: "/student-list/add" });
  };

  return (
    <div>
      <Header
        headerText="Students"
        description="This is a placeholder page for the students route"
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

        <Button variant={"outline"} onClick={handleAddStudent}>
          <UserPlusIcon />
          New Student
        </Button>
      </div>
      <div className="p-2">
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Name</TableHead>
              <TableHead>Age</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {isLoading ? (
              <TableRow>
                <TableCell
                  colSpan={2}
                  className="text-center text-muted-foreground"
                >
                  Loading...
                </TableCell>
              </TableRow>
            ) : students.length === 0 ? (
              <TableRow>
                <TableCell
                  colSpan={2}
                  className="text-center text-muted-foreground"
                >
                  No students found.
                </TableCell>
              </TableRow>
            ) : (
              students.map((s) => (
                <TableRow key={s.Id}>
                  <TableCell>{s.Name}</TableCell>
                  <TableCell>{s.Age}</TableCell>
                </TableRow>
              ))
            )}
          </TableBody>
        </Table>
      </div>
    </div>
  );
}
