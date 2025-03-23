export interface SkillDto {
  id: number;
  title: string;
  categories: string[];
}

export interface PersonaSkillDto {
  id: number;
  skillId: number;
  title: string;
  rating: number;
  categories: string[];
}
