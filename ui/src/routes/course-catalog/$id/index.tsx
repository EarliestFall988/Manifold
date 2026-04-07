import { createFileRoute, useNavigate } from "@tanstack/react-router";
import { useCourseByKey } from "@/hooks/Course";
import { Button } from "@/components/ui/button";
import { ArrowLeftIcon, BookOpenIcon, ClockIcon, StarIcon, UserIcon } from "@phosphor-icons/react";

export const Route = createFileRoute("/course-catalog/$id/")({
  component: RouteComponent,
});

function RouteComponent() {
  const { id } = Route.useParams();
  const nav = useNavigate();
  const { data: course, isLoading, isError } = useCourseByKey(Number(id));

  if (isLoading) return <div className="p-6">Loading...</div>;
  if (isError || !course) return <div className="p-6">Course not found.</div>;

  return (
    <div className="p-6 max-w-2xl space-y-6">
      <Button variant="ghost" size="sm" onClick={() => nav({ to: "/course-catalog" })}>
        <ArrowLeftIcon />
        Back to Catalog
      </Button>

      <div>
        <h1 className="text-2xl font-bold">{course.Name}</h1>
        <p className="text-muted-foreground mt-2">{course.Description}</p>
      </div>

      <div className="grid grid-cols-2 gap-4 sm:grid-cols-3">
        <div className="flex items-center gap-2 text-sm">
          <ClockIcon className="text-muted-foreground" />
          <span>{course.Weeks} weeks</span>
        </div>
        <div className="flex items-center gap-2 text-sm">
          <StarIcon className="text-muted-foreground" />
          <span>{course.Credits} credits</span>
        </div>
        {course.Instructor && (
          <div className="flex items-center gap-2 text-sm">
            <UserIcon className="text-muted-foreground" />
            <span>{course.Instructor.Name}</span>
          </div>
        )}
      </div>

      {course.Instructor && (
        <div className="border rounded-md p-4 space-y-1">
          <div className="flex items-center gap-2 font-medium">
            <BookOpenIcon />
            Instructor
          </div>
          <p className="text-sm">{course.Instructor.Name}</p>
          <p className="text-sm text-muted-foreground">{course.Instructor.Department}</p>
        </div>
      )}
    </div>
  );
}
