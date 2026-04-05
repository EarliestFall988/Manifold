
import { Header } from "@/components/header";
import { Button } from "@/components/ui/button";
import { createFileRoute } from "@tanstack/react-router";
import { useState } from "react";

export const Route = createFileRoute("/")({
  component: RouteComponent,
});

function RouteComponent() {

  const [count, setCount] = useState(0);

  return (
    <div className="">
      <Header headerText="Hello" description="Welcome to the react + asp.net application"/>
      <div className="rounded w-80 h-40 bg-card shadow p-5 border-border">
        <Button variant={"secondary"} onClick={() => setCount(count + 1)}>
          Count: {count}
        </Button>
      </div>
    </div>
  );
}
