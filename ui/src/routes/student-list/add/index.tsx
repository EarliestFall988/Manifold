import { Header } from "@/components/header";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { useCreateStudent } from "@/hooks/Student";
import { useForm } from "@tanstack/react-form";
import { createFileRoute, useNavigate } from "@tanstack/react-router";

export const Route = createFileRoute("/student-list/add/")({
  component: RouteComponent,
});

function RouteComponent() {
  const nav = useNavigate();
  const create = useCreateStudent();

  const form = useForm({
    defaultValues: {
      name: "",
      age: "",
    },
    onSubmit: async ({ value }) => {
      await create.mutateAsync({
        Id: 0,
        Name: value.name,
        Age: Number(value.age),
        Inserted: new Date().toISOString(),
        InsertedBy: "UI",
        Updated: null,
        UpdatedBy: null,
        Archived: false,
      });
      nav({ to: "/student-list" });
    },
  });

  return (
    <div>
      <Header headerText="New Student" description="Add a new student record." />

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
                placeholder="Jane Doe"
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
          name="age"
          validators={{
            onChange: ({ value }) => {
              if (value === "") return "Age is required";
              if (isNaN(Number(value)) || Number(value) <= 0)
                return "Age must be a positive number";
              return undefined;
            },
          }}
        >
          {(field) => (
            <div className="flex flex-col gap-1">
              <label className="text-sm font-medium" htmlFor={field.name}>
                Age
              </label>
              <Input
                id={field.name}
                type="number"
                value={field.state.value}
                onChange={(e) => field.handleChange(e.target.value)}
                onBlur={field.handleBlur}
                placeholder="21"
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
              {isSubmitting ? "Saving..." : "Add Student"}
            </Button>
          )}
        </form.Subscribe>
      </form>
    </div>
  );
}
