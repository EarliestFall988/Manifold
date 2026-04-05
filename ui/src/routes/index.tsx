import { Button } from "@/components/ui/button";
import { createFileRoute } from "@tanstack/react-router";
import { useState } from "react";

export const Route = createFileRoute("/")({
  component: RouteComponent,
});

function RouteComponent() {

  const [count, setCount] = useState(0);

  return (
    <div className="h-screen w-screen flex items-center bg-background justify-center">
      <div className="rounded w-1/5 h-40 bg-card shadow p-5 border-border">
        <h1 className="text-3xl font-semibold text-card-foreground">
         Hello!
        </h1>
        <Button variant={"secondary"} onClick={() => setCount(count + 1)}>
          Count: {count}
        </Button>
      </div>
    </div>
  );
}
