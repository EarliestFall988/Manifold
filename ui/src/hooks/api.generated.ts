// AUTO GENERATED with ❤️ by Api.TypeGen
// This file auto-generates React Query hooks for all API models.
// Last Generated: 2026-04-05 04:22:19 UTC

import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import type { ODataResponse } from "@/types/odata";
import type { WeatherForecast } from "@/types/api.generated";

const getWeatherForecast = (query?: string) =>
  axios
    .get<ODataResponse<WeatherForecast>>(`${import.meta.env.VITE_API_URL}/odata/WeatherForecast${query ? `?${query}` : ""}`)
    .then((res) => res.data);

export const useWeatherForecast = (query?: string) =>
  useQuery({
    queryKey: ["WeatherForecast", query],
    queryFn: () => getWeatherForecast(query),
  });

