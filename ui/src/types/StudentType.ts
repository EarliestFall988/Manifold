// AUTO GENERATED with ❤️ by Api.TypeGen
// Last Generated: 2026-04-11 21:07:16 UTC

import type { CourseType } from "./CourseType";

export interface StudentType {
  Id: number;
  Name: string;
  Age: number;
  GPA: string;
  Major: string;
  Email: string;
  Courses: CourseType[];
  Inserted: string;
  InsertedBy: string;
  Updated: string | null;
  UpdatedBy: string | null;
  Archived: boolean;
}
