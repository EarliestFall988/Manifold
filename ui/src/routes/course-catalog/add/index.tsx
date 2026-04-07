import { Header } from "@/components/header";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { useCreateCourse } from "@/hooks/Course";
import { useForm } from "@tanstack/react-form";
import { createFileRoute, useNavigate } from "@tanstack/react-router";

export const Route = createFileRoute("/course-catalog/add/")({
  component: RouteComponent,
});

function RouteComponent() {
  const nav = useNavigate();
  const create = useCreateCourse();

  const form = useForm({
    defaultValues: {
      name: "",
      description: "",
      weeks: "",
      credits: "",
    },
    onSubmit: async ({ value }) => {
      await create.mutateAsync({
        Id: 0,
        Name: value.name,
        Description: value.description,
        Weeks: Number(value.weeks),
        Credits: Number(value.credits),
        Instructor: null,
        Inserted: new Date().toISOString(),
        InsertedBy: "UI",
        Updated: null,
        UpdatedBy: null,
        Archived: false,
      });
      nav({ to: "/course-catalog" });
    },
  });

  return (
    <div>
      <Header headerText="New Course" description="Add a new course to the catalog." />

      <form
        className="flex flex-col gap-4 p-4 max-w-sm"
        onSubmit={(e) => {
          e.preventDefault();
          form.handleSubmit();
        }}
      >
        <form.Field
          name="name"
          validators={{
            onChange: ({ value }) =>
              value.trim().length === 0 ? "Name is required" : undefined,
          }}
        >
          {(field) => (
            <div className="flex flex-col gap-1">
              <label className="text-sm font-medium" htmlFor={field.name}>
                Name
              </label>
              <Input
                id={field.name}
                value={field.state.value}
                onChange={(e) => field.handleChange(e.target.value)}
                onBlur={field.handleBlur}
                placeholder="Introduction to Computer Science"
              />
              {field.state.meta.errors[0] && (
                <p className="text-destructive text-sm">
                  {field.state.meta.errors[0].toString()}
                </p>
              )}
            </div>
          )}
        </form.Field>

        <form.Field
          name="description"
          validators={{
            onChange: ({ value }) =>
              value.trim().length === 0 ? "Description is required" : undefined,
          }}
        >
          {(field) => (
            <div className="flex flex-col gap-1">
              <label className="text-sm font-medium" htmlFor={field.name}>
                Description
              </label>
              <Input
                id={field.name}
                value={field.state.value}
                onChange={(e) => field.handleChange(e.target.value)}
                onBlur={field.handleBlur}
                placeholder="A brief overview of the course"
              />
              {field.state.meta.errors[0] && (
                <p className="text-destructive text-sm">
                  {field.state.meta.errors[0].toString()}
                </p>
              )}
            </div>
          )}
        </form.Field>

        <form.Field
          name="weeks"
          validators={{
            onChange: ({ value }) => {
              if (value === "") return "Weeks is required";
              if (isNaN(Number(value)) || Number(value) <= 0)
                return "Weeks must be a positive number";
              return undefined;
            },
          }}
        >
          {(field) => (
            <div className="flex flex-col gap-1">
              <label className="text-sm font-medium" htmlFor={field.name}>
                Weeks
              </label>
              <Input
                id={field.name}
                type="number"
                value={field.state.value}
                onChange={(e) => field.handleChange(e.target.value)}
                onBlur={field.handleBlur}
                placeholder="16"
              />
              {field.state.meta.errors[0] && (
                <p className="text-destructive text-sm">
                  {field.state.meta.errors[0].toString()}
                </p>
              )}
            </div>
          )}
        </form.Field>

        <form.Field
          name="credits"
          validators={{
            onChange: ({ value }) => {
              if (value === "") return "Credits is required";
              if (isNaN(Number(value)) || Number(value) <= 0)
                return "Credits must be a positive number";
              return undefined;
            },
          }}
        >
          {(field) => (
            <div className="flex flex-col gap-1">
              <label className="text-sm font-medium" htmlFor={field.name}>
                Credits
              </label>
              <Input
                id={field.name}
                type="number"
                value={field.state.value}
                onChange={(e) => field.handleChange(e.target.value)}
                onBlur={field.handleBlur}
                placeholder="3"
              />
              {field.state.meta.errors[0] && (
                <p className="text-destructive text-sm">
                  {field.state.meta.errors[0].toString()}
                </p>
              )}
            </div>
          )}
        </form.Field>

        <form.Subscribe selector={(s) => s.isSubmitting}>
          {(isSubmitting) => (
            <Button type="submit" disabled={isSubmitting}>
              {isSubmitting ? "Saving..." : "Add Course"}
            </Button>
          )}
        </form.Subscribe>
      </form>
    </div>
  );
}
