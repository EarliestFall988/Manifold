import { Checkbox } from "@/components/ui/checkbox";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { useCourse, useUpdateCourse } from "@/hooks/Course";
import { createFileRoute } from "@tanstack/react-router";
import { useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { useStudent } from "@/hooks/Student";

export const Route = createFileRoute("/student-list/$id/")({
  component: RouteComponent,
});

function RouteComponent() {
  const { id } = Route.useParams();
  const studentId = Number(id);

  const {
    data,
    isLoading: studentLoading,
    isError,
  } = useStudent(`$filter=Id eq ${studentId}`);

  const student = data?.value?.[0];

  const queryClient = useQueryClient();

  const { data: coursesData, isLoading: coursesLoading } = useCourse();
  const { mutate: updateCourse, isPending } = useUpdateCourse();

  const allCourses = coursesData?.value ?? [];
  const enrolledIds = new Set(
    allCourses.filter((c) => c.StudentId === studentId).map((c) => c.Id),
  );

  const handleToggle = (courseId: number, enrolled: boolean) => {
    updateCourse(
      { key: courseId, delta: { StudentId: enrolled ? studentId : null } },
      {
        onSuccess: () => {
          queryClient.invalidateQueries({ queryKey: ["Course"] });
          toast.success(
            `Student ${enrolled ? "enrolled in" : "unenrolled from"} course successfully.`,
          );
        },
      },
    );
  };

  if (studentLoading) return <div className="p-6">Loading...</div>;
  if (isError || !student) return <div className="p-6">Student not found.</div>;

  return (
    <div className="p-6 space-y-6">
      <div>
        <h1 className="text-2xl font-bold">{student?.Name}</h1>
        <dl className="grid grid-cols-2 gap-x-4 gap-y-1 text-sm mt-2 max-w-xs">
          <dt className="text-muted-foreground">Age</dt>
          <dd>{student.Age}</dd>
          <dt className="text-muted-foreground">Inserted</dt>
          <dd>{new Date(student.Inserted).toLocaleDateString()}</dd>
        </dl>
      </div>

      <div>
        <h2 className="text-lg font-semibold mb-2">Course Enrollment</h2>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead className="w-10">Enrolled</TableHead>
              <TableHead>Name</TableHead>
              <TableHead>Description</TableHead>
              <TableHead>Weeks</TableHead>
              <TableHead>Credits</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {coursesLoading ? (
              <TableRow>
                <TableCell
                  colSpan={5}
                  className="text-center text-muted-foreground"
                >
                  Loading...
                </TableCell>
              </TableRow>
            ) : allCourses.length === 0 ? (
              <TableRow>
                <TableCell
                  colSpan={5}
                  className="text-center text-muted-foreground"
                >
                  No courses available.
                </TableCell>
              </TableRow>
            ) : (
              allCourses.map((c) => (
                <TableRow key={c.Id}>
                  <TableCell>
                    <Checkbox
                      checked={enrolledIds.has(c.Id)}
                      onCheckedChange={(checked: boolean) =>
                        handleToggle(c.Id, checked)
                      }
                      disabled={isPending}
                    />
                  </TableCell>
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
