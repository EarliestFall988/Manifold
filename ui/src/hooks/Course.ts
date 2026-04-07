// AUTO GENERATED with ❤️ by Api.TypeGen
// Last Generated: 2026-04-07 01:56:46 UTC

import { useQuery, useMutation } from "@tanstack/react-query";
import axios from "axios";
import type { ODataResponse } from "@/types/odata";
import type { CourseType } from "@/types/CourseType";

const getCourse = (query?: string) =>
  axios
    .get<ODataResponse<CourseType>>(`${import.meta.env.VITE_API_URL}/odata/Course${query ? `?${query}` : ""}`)
    .then((res) => res.data);

const getCourseByKey = (id: number) =>
  axios
    .get<ODataResponse<CourseType>>(
      `${import.meta.env.VITE_API_URL}/odata/Course?$filter=Id eq ${id}&$expand=Instructor`
    )
    .then((res) => res.data.value[0] ?? null);

export const useCourse = (query?: string) =>
  useQuery({
    queryKey: ["Course", query],
    queryFn: () => getCourse(query),
  });

export const useCourseByKey = (id: number) =>
  useQuery({
    queryKey: ["Course", id],
    queryFn: () => getCourseByKey(id),
  });

export const useCreateCourse = () =>
  useMutation({
    mutationFn: (item: CourseType) =>
      axios
        .post<CourseType>(`${import.meta.env.VITE_API_URL}/odata/Course`, item)
        .then((res) => res.data),
  });

export const useUpdateCourse = () =>
  useMutation({
    mutationFn: ({ key, delta }: { key: number; delta: Partial<CourseType> }) =>
      axios
        .patch<CourseType>(`${import.meta.env.VITE_API_URL}/odata/Course(${key})`, delta)
        .then((res) => res.data),
  });

export const useDeleteCourse = () =>
  useMutation({
    mutationFn: (key: number) =>
      axios.delete(`${import.meta.env.VITE_API_URL}/odata/Course(${key})`),
  });

