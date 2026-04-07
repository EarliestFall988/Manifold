// AUTO GENERATED with ❤️ by Api.TypeGen
// Last Generated: 2026-04-07 01:56:46 UTC

import { useQuery, useMutation } from "@tanstack/react-query";
import axios from "axios";
import type { ODataResponse } from "@/types/odata";
import type { WeatherForecastType } from "@/types/WeatherForecastType";

const getWeatherForecast = (query?: string) =>
  axios
    .get<ODataResponse<WeatherForecastType>>(`${import.meta.env.VITE_API_URL}/odata/WeatherForecast${query ? `?${query}` : ""}`)
    .then((res) => res.data);

export const useWeatherForecast = (query?: string) =>
  useQuery({
    queryKey: ["WeatherForecast", query],
    queryFn: () => getWeatherForecast(query),
  });

export const useCreateWeatherForecast = () =>
  useMutation({
    mutationFn: (item: WeatherForecastType) =>
      axios
        .post<WeatherForecastType>(`${import.meta.env.VITE_API_URL}/odata/WeatherForecast`, item)
        .then((res) => res.data),
  });

export const useUpdateWeatherForecast = () =>
  useMutation({
    mutationFn: ({ key, delta }: { key: number; delta: Partial<WeatherForecastType> }) =>
      axios
        .patch<WeatherForecastType>(`${import.meta.env.VITE_API_URL}/odata/WeatherForecast(${key})`, delta)
        .then((res) => res.data),
  });

export const useDeleteWeatherForecast = () =>
  useMutation({
    mutationFn: (key: number) =>
      axios.delete(`${import.meta.env.VITE_API_URL}/odata/WeatherForecast(${key})`),
  });

