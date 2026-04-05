// AUTO GENERATED with ❤️ by Api.TypeGen
// This file auto-generates React Query hooks for all API models.
// Last Generated: 2026-04-05 05:38:55 UTC

import { useQuery, useMutation } from "@tanstack/react-query";
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

export const useCreateWeatherForecast = () =>
  useMutation({
    mutationFn: (item: WeatherForecast) =>
      axios
        .post<WeatherForecast>(`${import.meta.env.VITE_API_URL}/odata/WeatherForecast`, item)
        .then((res) => res.data),
  });

export const useUpdateWeatherForecast = () =>
  useMutation({
    mutationFn: ({ key, delta }: { key: number; delta: Partial<WeatherForecast> }) =>
      axios
        .patch<WeatherForecast>(`${import.meta.env.VITE_API_URL}/odata/WeatherForecast(${key})`, delta)
        .then((res) => res.data),
  });

export const useDeleteWeatherForecast = () =>
  useMutation({
    mutationFn: (key: number) =>
      axios.delete(`${import.meta.env.VITE_API_URL}/odata/WeatherForecast(${key})`),
  });

