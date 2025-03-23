export interface CompanyDto {
  id: number;
  company: string;
  description: string;
  location: string;
  startDate: string;
  endDate?: string;
  positionCount: number;
}

export interface CompanyDetails extends CompanyDto {
  showTechnology: boolean;
  skills: CompanySkillDto[];
  positions: PositionDetails[];
}

export interface CompanyOptions {
  company: string;
  description: string;
  location: string;
  startDate: string;
  endDate?: string;
  positions: PositionOptions[];
  jobSkillIds: number[];
}

export interface CompanySkillDto {
  id: number;
  title: string;
}

export interface PositionDto {
  id: number;
  title: string;
  startDate: string;
  endDate?: string;
  highlightCount: number;
}

export interface PositionDetails extends PositionDto {
  highlights: HighlightDto[];
}

export interface PositionOptions {
  title: string;
  startDate: string;
  endDate?: string;
}

export interface HighlightDto {
  id: number;
  text: string;
}
