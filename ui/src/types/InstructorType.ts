// AUTO GENERATED with ❤️ by Api.TypeGen
// Last Generated: 2026-04-11 21:07:16 UTC

import type { CourseType } from "./CourseType";

export interface InstructorType {
  Id: number;
  Name: string;
  Department: string;
  Courses: CourseType[];
  Inserted: string;
  InsertedBy: string;
  Updated: string | null;
  UpdatedBy: string | null;
  Archived: boolean;
}
