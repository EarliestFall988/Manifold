import { useQuery } from "@tanstack/react-query";
import { getWeather } from "@/api/weather";

export const useWeather = () =>
  useQuery({
    queryKey: ["weather"],
    queryFn: getWeather,
  });
