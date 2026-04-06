import { Header } from "@/components/header";
import { useWeatherForecast } from "@/hooks/WeatherForecast";
import { useAutoAnimate } from "@formkit/auto-animate/react";
import { SpinnerGapIcon } from "@phosphor-icons/react";
import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/weather/")({
  component: RouteComponent,
});

function RouteComponent() {
  const [animParent] = useAutoAnimate();

  const { data, isLoading, error } = useWeatherForecast();

  return (
    <div ref={animParent} className="flex flex-col gap-0">
      <Header headerText="Weather" description="This is fetching data from the asp.net backend using odata" />
      {isLoading && (
        <div className="flex w-full h-30 items-center justify-center">
          <SpinnerGapIcon className="animate-spin" />
        </div>
      )}

      {error && <p>Error: {error.message}</p>}
      {!isLoading && data && (
        <div className="flex gap-2 flex-col items-start justify-start">
          <p className="text-lg font-semibold">Overview</p>
          {data.value.map((forecast) => (
            <div
              key={forecast.Id}
              className="bg-card w-80 rounded p-3 border-border border"
            >
              <p className="text-sm text-muted-foreground">
                {new Date(forecast.Date).toLocaleDateString()}
              </p>
              <div className="flex gap-2">
                <p>{forecast.TemperatureC}°C</p>
                <div className="border-border border px-2 rounded-full">
                  <p className="text-sm">{forecast.Summary}</p>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
